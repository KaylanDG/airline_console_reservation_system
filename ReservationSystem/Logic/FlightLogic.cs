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
        List<Flight> availableFlightsForDestination = new List<Flight>();

        // For each available flight check if the destination is equel to the given argument
        // if so add the flight to the list
        foreach (Flight flight in GetAvailableFlights())
        {
            if (flight.Destination.ToLower() == destination.ToLower())
            {
                availableFlightsForDestination.Add(flight);
            }
        }

        return availableFlightsForDestination;
    }

    public Flight GetFlightByFlightNumber(string number)
    {
        List<Flight> flights = GetAvailableFlights();
        foreach (Flight flight in flights)
        {
            if (flight.FlightNumber == number)
            {
                return flight;
            }
        }

        return null;
    }
}