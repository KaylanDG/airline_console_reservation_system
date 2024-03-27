static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private List<Flight> _availableFlights = _flightLogic.GetAvailableFlights();
    static private AccountsLogic _accountsLogic = new AccountsLogic();
    static private ReservationLogic _reservationLogic = new ReservationLogic();

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
        }
        else
        {
            Console.WriteLine("\nNo flights found!\n");
        }

        SmallMenu();
    }

    public static void SmallMenu()
    {
        Console.WriteLine("\nChoose one of the menu options:");
        Console.WriteLine(new string('-', 20));
        Console.WriteLine("G | Go back");
        Console.WriteLine("S | Search for flights");
        if (AccountsLogic.CurrentAccount != null)
        {
            Console.WriteLine("M | Make reservation");
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
        else if (choice == "m" && AccountsLogic.CurrentAccount != null)
        {
            // Start reservation menu
            ReservationMenu.Start();
        }
        else
        {
            _availableFlights = _flightLogic.GetAvailableFlights();
            Console.WriteLine("\nInvalid choice. Please try again.\n");
        }
    }
}