static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private List<Flight> _availableFlights = _flightLogic.GetAvailableFlights();

    public static void Start()
    {
        Console.Clear();
        for (int i = 0; i < _availableFlights.Count + 2; i++)
        {
            if (i == 0)
            {
                Console.WriteLine("{0,-20} | {1, -15} | {2,-15} {3,-20} -->   {4,-15} {5,-20}", "AIRLINES", "FLIGHT NUMBER", "FROM", "DEPARTURE", "TO", "ARRIVAL");
            }
            else if (i == 1)
            {
                string line = new string('-', 120);
                Console.WriteLine(line);
            }
            else
            {
                Flight flight = _availableFlights[i - 2];
                Console.WriteLine("{0,-20} | {1, -15} | {2,-15} {3,-20} -->   {4,-15} {5,-20}", flight.Plane.Airline, flight.FlightNumber, flight.From, flight.DepartureTime, flight.Destination, flight.ArrivalTime);
            }
        }
    }
}