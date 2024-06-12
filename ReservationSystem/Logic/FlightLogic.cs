using System.Globalization;
using System.Text.RegularExpressions;

public class FlightLogic
{
    private List<FlightModel> _flights;
    private List<PlaneModel> _planes;
    private PlaneLogic _planeLogic;
    private FlightsAccess _flightsAccess = new FlightsAccess();
    private PlaneAccess _planesAccess = new PlaneAccess();
    private ReservationAccess _reservationAccess = new ReservationAccess();


    public FlightLogic()
    {
        // Load in all flights
        _flights = _flightsAccess.LoadAll();
        _planes = _planesAccess.LoadAll();
        _planeLogic = new PlaneLogic();

    }

    public List<FlightModel> GetAvailableFlights()
    {
        List<FlightModel> availableFlights = new List<FlightModel>();
        string format = "dd-MM-yyyy HH:mm";


        // For each flight check if the departure date hasn't passed.
        // if not add the flight to availableFlights.
        foreach (FlightModel flight in _flightsAccess.LoadAll())
        {
            string departureDateTimeString = flight.DepartureTime;

            DateTime departureDateTime = DateTime.ParseExact(departureDateTimeString, format, System.Globalization.CultureInfo.InvariantCulture);
            DateTime today = DateTime.Now;

            if (departureDateTime >= today)
            {
                availableFlights.Add(flight);
            }
        }

        return availableFlights.OrderBy(x => DateTime.ParseExact(x.DepartureTime, format, System.Globalization.CultureInfo.InvariantCulture)).ToList();
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

    public bool CreateFlights(string flightNumber, int planeID, string from, string destination, string timezone, int duration, string startDate, int flightAmount)
    {
        DateTime departure = DateTime.ParseExact(startDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        DateTime arrival = departure.AddMinutes(duration);

        //Convert arrival time to timezone of destination
        TimeZoneInfo destTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        arrival = TimeZoneInfo.ConvertTime(arrival, TimeZoneInfo.Utc, destTimeZone);


        for (int i = 0; i < flightAmount; i++)
        {
            FlightModel flight = new FlightModel(
                GenerateFlightId(),
                flightNumber,
                from,
                destination,
                departure.ToString("dd-MM-yyyy HH:mm"),
                duration,
                arrival.ToString("dd-MM-yyyy HH:mm"),
                planeID
            );

            UpdateList(flight);

            departure = departure.AddDays(1);
            arrival = arrival.AddDays(1);
        }

        //Update the flight overview with new flights
        FlightOverview._flights = GetAvailableFlights();
        return true;
    }

    public bool RemoveFlight(int flightid)
    {
        List<FlightModel> _flights = _flightsAccess.LoadAll();
        bool removed = false;
        foreach (FlightModel x in _flights)
        {
            if (x.Id == flightid)
            {
                _flights.Remove(x);
                removed = true;
                break;
            }
        }
        _flightsAccess.WriteAll(_flights);
        return removed;
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

        _flightsAccess.WriteAll(_flights);
    }

    private int GenerateFlightId()
    {
        // Load existing reservations from the JSON file
        var flights = _flightsAccess.LoadAll();

        // Find the highest existing ID
        int maxId = flights.Max(r => r.Id);

        // Increment the highest existing ID by one to generate a new ID
        return maxId + 1;
    }
    public List<ReservationModel> GetFlightReservations(FlightModel flight)
    {
        List<ReservationModel> flightReservations = new List<ReservationModel>();

        foreach (var reservation in _reservationAccess.LoadAll())
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
        List<SeatModel> flightSeats = _planeLogic.GetPlaneSeats(flight.Plane);
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

    public bool IsValidInt(string y)
    {
        //Check if possible to pass to int
        //If not return false
        if (!int.TryParse(y, out int num))
        {
            return false;
        }

        //Convert to int
        int x = Convert.ToInt32(y);
        if (x <= 0)
        {
            return false;
        }

        //else return true
        return true;
    }

    public bool IsValidDateFormat(string input)
    {
        // Check if date has correct format
        string format = "dd-MM-yyyy HH:mm";
        DateTime parsedDate;
        //return true or false
        return DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out parsedDate);
    }

    public bool IsValidDate(string input)
    {
        //check if date is not in the past
        DateTime now = DateTime.Now;
        DateTime date = DateTime.ParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

        if (date < now) return false;
        return true;
    }

    public bool IsValidTimeZone(string timezone)
    {
        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return true;
        }
        catch (TimeZoneNotFoundException)
        {
            return false;
        }
    }

    public string GetTimeZoneID(string timezone)
    {
        switch (timezone)
        {
            case "UTC-12:00":
                return "Dateline Standard Time";
            case "UTC-11:00":
                return "UTC-11";
            case "UTC-10:00":
                return "Hawaiian Standard Time";
            case "UTC-09:00":
                return "Alaskan Standard Time";
            case "UTC-08:00":
                return "Pacific Standard Time";
            case "UTC-07:00":
                return "Mountain Standard Time";
            case "UTC-06:00":
                return "Central Standard Time";
            case "UTC-05:00":
                return "Eastern Standard Time";
            case "UTC-04:00":
                return "Atlantic Standard Time";
            case "UTC-03:00":
                return "Argentina Standard Time";
            case "UTC-02:00":
                return "UTC-02";
            case "UTC-01:00":
                return "Azores Standard Time";
            case "UTC±00:00":
                return "UTC";
            case "UTC+01:00":
                return "Central European Standard Time";
            case "UTC+02:00":
                return "E. Europe Standard Time";
            case "UTC+03:00":
                return "Russian Standard Time";
            case "UTC+04:00":
                return "Arabian Standard Time";
            case "UTC+05:00":
                return "Pakistan Standard Time";
            case "UTC+06:00":
                return "Central Asia Standard Time";
            case "UTC+07:00":
                return "SE Asia Standard Time";
            case "UTC+08:00":
                return "China Standard Time";
            case "UTC+09:00":
                return "Tokyo Standard Time";
            case "UTC+10:00":
                return "AUS Eastern Standard Time";
            case "UTC+11:00":
                return "Central Pacific Standard Time";
            case "UTC+12:00":
                return "UTC+12";
            default:
                return "Invalid time zone";
        }
    }
    public List<FlightModel> GetFlightsForPage(int page, int pageSize, List<FlightModel> flights)
    {
        //Get the starting index
        int startIndex = (page - 1) * pageSize;
        //Get the size of the sublist
        int count = Math.Min(pageSize, flights.Count - startIndex);
        // return sublist
        Console.WriteLine($"{startIndex} {count}");
        return flights.GetRange(startIndex, count);
    }

    public int MaxLuggageAmount(int flightID)
    {
        FlightModel flight = GetById(flightID);
        List<ReservationModel> flightResevations = GetFlightReservations(flight);
        PlaneModel plane = GetPlaneByID(flight.Plane);
        int maxLuggageAmount = plane.LuggageAmount;

        foreach (ReservationModel reservation in flightResevations)
        {
            foreach (PassengerModel passenger in reservation.Passengers)
            {
                if (passenger.AdditionalServices.Count > 0)
                {
                    maxLuggageAmount -= passenger.AdditionalServices.Sum(x => x.Quantity);
                }
            }
        }


        return maxLuggageAmount;
    }

    public bool IsValidFlightNumber(string flightNumber)
    {
        Regex flightnumberRegex = new Regex(@"^[A-Za-z]{2}\d{4}$");

        if (flightnumberRegex.IsMatch(flightNumber)) return true;

        return false;
    }
}