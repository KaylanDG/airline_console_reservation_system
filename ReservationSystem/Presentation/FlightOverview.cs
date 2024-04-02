static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private List<FlightModel> _availableFlights = _flightLogic.GetAvailableFlights();

    public static void Start()
    {
        ShowOverview();
        FlightOverviewMenu();
    }

    public static void ShowOverview()
    {
        Console.Clear();

        // top part of overview
        Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20}", "", "AIRLINES", "FLIGHT NUMBER", "FROM", "DEPARTURE", "TO", "ARRIVAL");
        Console.WriteLine(new string('-', 120));

        // Show flights if there are any
        if (_availableFlights.Count > 0)
        {
            for (int i = 0; i < _availableFlights.Count; i++)
            {
                FlightModel flight = _availableFlights[i];
                Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20}", flight.Id, flight.Plane.Airline, flight.FlightNumber, flight.From, flight.DepartureTime, flight.Destination, flight.ArrivalTime);

            }
        }
        else
        {
            Console.WriteLine("\nNo flights found!\n");
        }
    }

    public static void FlightOverviewMenu()
    {
        Console.WriteLine("\nChoose one of the menu options:");
        Console.WriteLine(new string('-', 20));
        Console.WriteLine("G | Go back");
        Console.WriteLine("S | Search for flights");
        if (AccountsLogic.CurrentAccount != null)
        {
            Console.WriteLine("R | Make reservation");
        }

        string choice = Console.ReadLine().ToLower();

        if (choice == "g")
        {
            // If user goes back to main menu set the value of _availableFlights back to all available flights
            // So that is doesnt show the search results if the user goes back to the flight overview
            _availableFlights = _flightLogic.GetAvailableFlights();
            Menu.Start();
        }
        else if (choice == "s")
        {
            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            // Set _availableFlights value to the returned list of flights for destination
            _availableFlights = _flightLogic.GetAvailableFlightsForDestination(destination);
            // Start the overview again so that it shows the search results
            Start();
        }
        else if (choice == "r" && AccountsLogic.CurrentAccount != null)
        {
            // Start reservation menu
            Reservation.Start();
        }
        else
        {
            _availableFlights = _flightLogic.GetAvailableFlights();
            Console.WriteLine("\nInvalid choice. Please try again.\n");
        }
    }
}