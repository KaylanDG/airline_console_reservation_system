using System.Globalization;

public class FlightLogic
{
    private List<FlightModel> _flights;
    private FlightLogic _flightLogic;
    private List<PlaneModel> _planes;



    public FlightLogic()
    {
        // Load in all flights
        _flights = FlightsAccess.LoadAllFlights();
        _planes = PlaneAccess.LoadAllPlanes();

    }

    public List<FlightModel> GetAvailableFlights()
    {
        List<FlightModel> availableFlights = new List<FlightModel>();

        // For each flight check if the departure date hasn't passed.
        // if not add the flight to availableFlights.
        foreach (FlightModel flight in FlightsAccess.LoadAllFlights())
        {
            string departureDateTimeString = flight.DepartureTime;
            string format = "dd-MM-yyyy HH:mm";

            DateTime departureDateTime = DateTime.ParseExact(departureDateTimeString, format, System.Globalization.CultureInfo.InvariantCulture);
            DateTime today = DateTime.Now;

            if (departureDateTime >= today)
            {
                availableFlights.Add(flight);
            }
        }

        return availableFlights;
    }

    public List<FlightModel> GetAvailableFlightsForDestination(string destination)
    {
        List<FlightModel> availableFlightsForDestination = new List<FlightModel>();

        // For each available flight check if the destination is equel to the given argument
        // if so add the flight to the list
        foreach (FlightModel flight in GetAvailableFlights())
        {
            if (flight.Destination.ToLower() == destination.ToLower())
            {
                availableFlightsForDestination.Add(flight);
            }
        }

        return availableFlightsForDestination;
    }

    public FlightModel GetById(int id)
    {
        return _flights.Find(i => i.Id == id);
    }

    public PlaneModel GetPlaneByID(int id)
    {

        foreach (PlaneModel plane in _planes)
        {
            if (id == plane.Id)
            {
                return plane;
            }
        }
        return null;
    }

    public List<FlightModel> CreateMultipleFlights(int howmany, string flightnumber, string from, string destination, string departure_time, int flight_duration, string arrival_time, int planeId)
    {
        List<FlightModel> createdFlights = new List<FlightModel>();

        // Parse the initial departure time
        DateTime initialDepartureTime = DateTime.ParseExact(departure_time, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

        // Iterate to create multiple flights
        for (int i = 0; i < howmany; i++)
        {
            DateTime currentDepartureTime = initialDepartureTime.AddDays(i);

            DateTime currentArrivalTime = currentDepartureTime.AddMinutes(flight_duration);

            // Create the flight
            FlightModel newFlight = new FlightModel(
                GenerateFlightId(),
                flightnumber,
                from,
                destination,
                currentDepartureTime.ToString("dd-MM-yyyy HH:mm"),
                flight_duration,
                currentArrivalTime.ToString("dd-MM-yyyy HH:mm"),
                planeId
            );

            createdFlights.Add(newFlight);

            UpdateList(newFlight);
        }

        return createdFlights;
    }
    public FlightModel CreateFlight(string flightnumber, string from, string destination, string departure_time, int flight_duration, string arrival_time, int id)
    {
        FlightModel newFlight = new FlightModel(
            GenerateFlightId(),
            flightnumber,
            from,
            destination,
            departure_time,
            flight_duration,
            arrival_time,
            id
        );

        UpdateList(newFlight);
        return newFlight;
    }

    public bool IsPlaneAvailable(DateTime departure, DateTime arrival, int planeID, int flightAmount)
    {
        bool available = true;

        for (int i = 1; i <= flightAmount; i++)
        {
            foreach (FlightModel flight in _flights)
            {
                if (flight.Plane == planeID)
                {
                    string format = "dd-MM-yyyy HH:mm";
                    DateTime flightDeparture = DateTime.ParseExact(flight.DepartureTime, format, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime flightArrival = DateTime.ParseExact(flight.ArrivalTime, format, System.Globalization.CultureInfo.InvariantCulture);

                    if (departure >= flightDeparture && departure <= flightArrival)
                    {
                        available = false;
                    }

                    if (arrival >= flightDeparture && arrival <= flightArrival)
                    {
                        available = false;
                    }
                }
            }

            departure.AddDays(1);
            arrival.AddDays(1);
        }
        return available;
    }

    public bool IsPlaneAvailable(DateTime departure, DateTime arrival, int planeID)
    {
        return IsPlaneAvailable(departure, arrival, planeID, 1);
    }

    public bool DoesFlightExist(int flightID)
    {
        // Check if given flight number is in the list of available flights
        foreach (var flight in GetAvailableFlights())
        {
            if (flight.Id == flightID)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateList(FlightModel flight)
    {
        int index = _flights.FindIndex(r => r.Id == flight.Id);

        if (index != -1)
        {
            _flights[index] = flight;
        }
        else
        {
            _flights.Add(flight);
        }

        FlightsAccess.WriteAll(_flights);
    }

    private int GenerateFlightId()
    {
        // Load existing reservations from the JSON file
        var flights = FlightsAccess.LoadAllFlights();

        // Find the highest existing ID
        int maxId = flights.Max(r => r.Id);

        // Increment the highest existing ID by one to generate a new ID
        return maxId + 1;
    }
    public List<ReservationModel> GetFlightReservations(FlightModel flight)
    {
        List<ReservationModel> flightReservations = new List<ReservationModel>();

        foreach (var reservation in ReservationAccess.LoadAll())
        {
            if (reservation.FlightId == flight.Id)
            {
                flightReservations.Add(reservation);
            }
        }

        return flightReservations;
    }

    public List<SeatModel> GetFlightSeats(FlightModel flight)
    {
        PlaneModel plane = GetPlaneByID(flight.Plane);
        List<SeatModel> flightSeats = plane.GetPlaneSeats();
        List<ReservationModel> flightReservations = GetFlightReservations(flight);

        foreach (SeatModel seat in flightSeats)
        {
            bool reserved = flightReservations.Any(reservation => reservation.Passengers.Any(passenger => passenger.SeatNumber == seat.SeatNumber));

            if (reserved)
            {
                seat.IsReserved = true;
            }
        }
        return flightSeats;
    }

    public List<SeatModel> UpdateFlightSeats(string seatNumber, List<SeatModel> flightSeats)
    {
        foreach (SeatModel seat in flightSeats)
        {
            if (seat.SeatNumber == seatNumber)
            {
                seat.IsSelected = true;
            }
        }

        return flightSeats;
    }

    public bool IsSeatReserved(string seatNumber, List<SeatModel> seats)
    {
        return seats.Any(seat => seat.SeatNumber == seatNumber && seat.IsReserved);
    }

    public bool IsSeatSelected(string seatNumber, List<SeatModel> seats)
    {
        return seats.Any(seat => seat.SeatNumber == seatNumber && seat.IsSelected);
    }

    public double GetSeatPrice(string seatNumber, FlightModel flight)
    {
        List<SeatModel> flightSeats = GetFlightSeats(flight);
        SeatModel seat = flightSeats.Find(i => i.SeatNumber == seatNumber)!;
        return seat.PricePerMinute * flight.FlightDuration;
    }

    public List<FlightModel> GetReturnFlights(FlightModel flight)
    {
        List<FlightModel> returnFlights = new List<FlightModel>();
        foreach (FlightModel f in GetAvailableFlights())
        {
            if (f.From == flight.Destination && f.Destination == "Rotterdam")
            {
                returnFlights.Add(f);
            }
        }
        return returnFlights;
    }
}