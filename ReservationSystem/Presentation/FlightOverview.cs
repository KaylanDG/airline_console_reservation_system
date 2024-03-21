static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private List<Flight> _availableFlights = _flightLogic.GetAvailableFlights();

    public static void Start()
    {
        Console.Clear();

        // top part of overview
        Console.WriteLine("{0,-20} | {1, -15} | {2,-15} {3,-20} -->   {4,-15} {5,-20}", "AIRLINES", "FLIGHT NUMBER", "FROM", "DEPARTURE", "TO", "ARRIVAL");
        Console.WriteLine(new string('-', 120));

        // Show flights if there are any
        if (_availableFlights.Count > 0)
        {
            for (int i = 0; i < _availableFlights.Count; i++)
            {
                Flight flight = _availableFlights[i];
                Console.WriteLine("{0,-20} | {1, -15} | {2,-15} {3,-20} -->   {4,-15} {5,-20}", flight.Plane.Airline, flight.FlightNumber, flight.From, flight.DepartureTime, flight.Destination, flight.ArrivalTime);

            }
            SmallMenu();
        }
        else
        {
            Console.WriteLine("\nNo flights found!\n");
        }
    }

    public static void SmallMenu()
    {
        Console.WriteLine("\nChoose one of the menu options:");
        Console.WriteLine(new string('-', 20));
        Console.WriteLine("G | Go back");
        Console.WriteLine("S | Search for flights");
        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            return;
        }
        else if (choice == 2)
        {
            return;
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

    }
}