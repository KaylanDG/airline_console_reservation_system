static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private int _selectedOption = 0;
    static private List<string> _options;
    static public List<FlightModel> _flights = _flightLogic.GetAvailableFlights();
    private static List<ReservationModel> _reservations;
    private static int _page;

    private static int _pageAmount;
    public static void Start()
    {
        _options = new List<string>() { "Go back", "Search for flights", };
        if (AccountsLogic.CurrentAccount != null) _options.Add("Make reservation");
        _page = 1;


        _flights = _flightLogic.GetFlightsForPage(_page, 10);
        _pageAmount = (int)Math.Ceiling((double)_flights.Count / 10);
        ConsoleKey pressedKey = default;
        while (pressedKey != ConsoleKey.Enter)
        {
            Console.Clear();
            ShowOverview();
            DisplayMenu();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.UpArrow)
            {
                _selectedOption--;
                if (_selectedOption == -1) _selectedOption = _options.Count - 1;
            }
            else if (pressedKey == ConsoleKey.DownArrow)
            {
                _selectedOption++;
                if (_selectedOption == _options.Count) _selectedOption = 0;
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                if (_page < _pageAmount)
                {
                    _page++;
                }
                _flights = _flightLogic.GetFlightsForPage(_page, 10);
            }
            else if (pressedKey == ConsoleKey.LeftArrow)
            {
                if (_page > 1)
                {
                    _page--;
                }
                _flights = _flightLogic.GetFlightsForPage(_page, 10);
            }
        }

        if (_selectedOption == 0)
        {
            _flights = _flightLogic.GetAvailableFlights();
            Console.Clear();
            MainMenu.Start();
            return;
        }
        else if (_selectedOption == 1)
        {
            SearchForFlight();
        }

        else if (_selectedOption == 2)
        {
            Reservation.Start();
        }
    }

    public static void ShowOverview(int selectedFlight = -1)
    {
        // top part of overview
        Console.WriteLine();
        Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20} | {7,-20}", "", "AIRLINES", "FLIGHT NUMBER", "FROM", "DEPARTURE", "TO", "ARRIVAL", "RETURN FLIGHTS");
        Console.WriteLine(new string('-', 141));

        // Show flights if there are any
        if (_flights.Count > 0)
        {
            for (int i = 0; i < _flights.Count; i++)
            {
                FlightModel flight = _flights[i];
                int amountOfReturnFLights = _flightLogic.GetReturnFlights(flight).Count;

                if (selectedFlight == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20} | {7,-20}", flight.Id, _flightLogic.GetPlaneByID(flight.Plane).Airline, flight.FlightNumber, flight.From, flight.DepartureTime, flight.Destination, flight.ArrivalTime, amountOfReturnFLights);

                Console.ResetColor();
            }
            Console.WriteLine($"{_page}/{_pageAmount}");
        }
        else
        {
            Console.WriteLine("\nNo flights found!\n");
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\nChoose an option from the menu:\n");

        for (int i = 0; i < _options.Count; i++)
        {
            string prefix = " ";
            if (i == _selectedOption)
            {
                prefix = ">";
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{prefix} {_options[i]}");
            Console.ResetColor();
        }
    }

    private static void SearchForFlight()
    {
        Console.Clear();
        Console.WriteLine("\nEnter a destination (e.g. London):");
        string destination = Console.ReadLine();
        _flights = _flightLogic.GetAvailableFlightsForDestination(destination);
        Start();
        return;
    }

    public static int SelectFlight()
    {
        ConsoleKey pressedKey = default;
        int selectedFlight = 0;
        _page = 1;
        _pageAmount = (int)Math.Ceiling((double)_flights.Count / 10);
        while (pressedKey != ConsoleKey.Enter)
        {
            _flights = _flightLogic.GetFlightsForPage(_page, 10);
            Console.Clear();
            ShowOverview(selectedFlight);
            Console.WriteLine("\nSelect a flight.\nUse the arrow keys to navigate, press enter to select a flight.");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.UpArrow)
            {
                selectedFlight--;
                if (selectedFlight == -1) selectedFlight = _flights.Count - 1;
            }
            else if (pressedKey == ConsoleKey.DownArrow)
            {
                selectedFlight++;
                if (selectedFlight == _flights.Count) selectedFlight = 0;
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                if (_page < _pageAmount)
                {
                    _page++;
                }
                _flights = _flightLogic.GetFlightsForPage(_page, 10);
            }
            else if (pressedKey == ConsoleKey.LeftArrow)
            {
                if (_page > 1)
                {
                    _page--;
                }
                _flights = _flightLogic.GetFlightsForPage(_page, 10);
            }
        }

        return _flights[selectedFlight].Id;
    }

}