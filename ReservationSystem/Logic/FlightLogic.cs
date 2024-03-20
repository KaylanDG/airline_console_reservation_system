class FlightLogic
{
    private List<Flight> _flights;

    public FlightLogic()
    {
        // Load in all flights
        _flights = FlightsAccess.LoadAllFlights();
    }

    public List<Flight> GetAvailableFlights()
    {
        List<Flight> availableFlights = new List<Flight>();

        // For each flight check if the departure date hasn't passed.
        // if not add the flight to availableFlights.
        foreach (Flight flight in _flights)
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

    public List<Flight> GetAvailableFlightsForDestination(string destination)
    {
        List<Flight> availableFlights = new List<Flight>();

        foreach (Flight flight in GetAvailableFlights())
        {

        }

        return availableFlights;
    }
}