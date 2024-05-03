static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private List<FlightModel> _availableFlights;

    public static void Start()
    {
        _availableFlights = _flightLogic.GetAvailableFlights();
        ShowOverview();
        FlightOverviewMenu();
    }
    public static void ShowOverview(List<FlightModel> flights)
    {
        Console.Clear();

        // top part of overview
        Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20}", "", "AIRLINES", "FLIGHT NUMBER", "FROM", "DEPARTURE", "TO", "ARRIVAL");
        Console.WriteLine(new string('-', 120));

        // Show flights if there are any
        if (flights.Count > 0)
        {
            for (int i = 0; i < flights.Count; i++)
            {
                FlightModel flight = flights[i];
                Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20}", flight.Id, _flightLogic.GetPlaneByID(flight.Plane).Airline, flight.FlightNumber, flight.From, flight.DepartureTime, flight.Destination, flight.ArrivalTime);

            }
        }
        else
        {
            Console.WriteLine("\nNo flights found!\n");
        }
    }

    public static void ShowOverview()
    {
        ShowOverview(_flightLogic.GetAvailableFlights());
    }

    public static void FlightOverviewMenu()
    {
        Console.WriteLine("\nChoose one of the menu options:");
        Console.WriteLine(new string('-', 20));
        Console.WriteLine("G | Go back");
        Console.WriteLine("S | Search for flights");
        Console.WriteLine("T | Tomorrow's flight(s)");
        if (AccountsLogic.CurrentAccount != null)
        {
            Console.WriteLine("R | Make reservation");
        }

        string choice = Console.ReadLine().ToLower();

        if (choice == "g")
        {
            Menu.Start();
        }
        else if (choice == "s")
        {
            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            // Set _availableFlights value to the returned list of flights for destination
            _availableFlights = _flightLogic.GetAvailableFlightsForDestination(destination);
            ShowOverview();
            FlightOverviewMenu();
        }


        else if (choice == "r" && AccountsLogic.CurrentAccount != null)
        {
            // Start reservation menu
            Reservation.Start();
        }
        else
        {
            Console.WriteLine("\nInvalid choice. Please try again.\n");
        }
    }
}