public class FlightLogic
{
    private List<FlightModel> _flights;

    public FlightLogic()
    {
        // Load in all flights
        _flights = FlightsAccess.LoadAllFlights();
    }

    public List<FlightModel> GetAvailableFlights()
    {
        List<FlightModel> availableFlights = new List<FlightModel>();

        // For each flight check if the departure date hasn't passed.
        // if not add the flight to availableFlights.
        foreach (FlightModel flight in _flights)
        {
            string departureDateTimeString = flight.DepartureTime;
            string format = "dd-MM-yyyy HH:mm tt";

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
        List<FlightModel> flights = GetAvailableFlights();
        return flights.Find(i => i.Id == id);
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

}