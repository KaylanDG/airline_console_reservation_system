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
        Console.WriteLine("M | Make reservation");
        string choice = Console.ReadLine().ToLower();

        if (choice == "g")
        {
            Menu.Start();
        }
        else if (choice == "s")
        {
            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();
            _availableFlights = _flightLogic.GetAvailableFlightsForDestination(destination);
            Start();
        }
        else if (choice == "m")
        {
            if (AccountsLogic.CurrentAccount != null)
            {
                Console.Write("Enter flight number: ");
                string flightNumber = Console.ReadLine();
                if (_reservationLogic.DoesFlightExist(flightNumber))
                {
                    Console.WriteLine($"Reservation made for flight number: {flightNumber}");
                }
                else
                {
                    Console.WriteLine("Flight number does not exist. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("You must be logged in to make a reservation.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}