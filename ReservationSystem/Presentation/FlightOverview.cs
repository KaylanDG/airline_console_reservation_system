static class FlightOverview
{
    static private FlightLogic _flightLogic = new FlightLogic();
    static private int _selectedOption = 0;
    static private List<string> _options;
    static public List<FlightModel> _flights = _flightLogic.GetAvailableFlights();
    static public List<FlightModel> _flightsPerPage;
    private static int _page = 1;
    private static int _pageAmount = (int)Math.Ceiling((double)_flights.Count / 10);
    public static void Start()
    {
        _flights = _flightLogic.GetAvailableFlights();

        _options = new List<string>() { "Go back", "Search for flights", };
        if (AccountsLogic.CurrentAccount != null) _options.Add("Make reservation");

        _page = 1;

        ShowOverview();
    }

    public static void ShowOverview()
    {
        _pageAmount = (int)Math.Ceiling((double)_flights.Count / 10);
        ConsoleKey pressedKey = default;
        while (pressedKey != ConsoleKey.Enter)
        {
            Console.Clear();
            Flights();
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
                if (_page < _pageAmount) _page++;
                _flightsPerPage = _flightLogic.GetFlightsForPage(_page, 10, _flights);
            }
            else if (pressedKey == ConsoleKey.LeftArrow)
            {
                if (_page > 1) _page--;
                _flightsPerPage = _flightLogic.GetFlightsForPage(_page, 10, _flights);
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

    public static void Flights(int selectedFlight = -1)
    {
        if (_flights == null) _flights = _flightLogic.GetAvailableFlights();
        _flightsPerPage = _flightLogic.GetFlightsForPage(_page, 10, _flights);

        // top part of overview
        Console.WriteLine();
        Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20} | {7,-20}", "", "AIRLINES", "FLIGHT NUMBER", "FROM", "DEPARTURE", "TO", "ARRIVAL", "RETURN FLIGHTS");
        Console.WriteLine(new string('-', 141));

        // Show flights if there are any
        if (_flightsPerPage.Count > 0)
        {
            for (int i = 0; i < _flightsPerPage.Count; i++)
            {
                FlightModel flight = _flightsPerPage[i];
                int amountOfReturnFLights = _flightLogic.GetReturnFlights(flight).Count;

                if (selectedFlight == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine("{0,-5} {1,-20} | {2, -15} | {3,-15} {4,-20} -->   {5,-15} {6,-20} | {7,-20}", flight.Id, _flightLogic.GetPlaneByID(flight.Plane).Airline, flight.FlightNumber, flight.From, flight.DepartureTime, flight.Destination, flight.ArrivalTime, amountOfReturnFLights);

                Console.ResetColor();
            }
            Console.WriteLine($"\nPage: {_page}/{_pageAmount}");
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
        ShowOverview();
        return;
    }

    public static int SelectFlight()
    {
        if (_flights == null) _flights = _flightLogic.GetAvailableFlights();

        ConsoleKey pressedKey = default;
        int selectedFlight = 0;
        _page = 1;
        _pageAmount = (int)Math.Ceiling((double)_flights.Count / 10);
        while (pressedKey != ConsoleKey.Enter)
        {
            _flightsPerPage = _flightLogic.GetFlightsForPage(_page, 10, _flights);
            Console.Clear();
            Flights(selectedFlight);
            Console.WriteLine("\nSelect a flight.\nUse the arrow keys to navigate, press enter to select a flight.");
            Console.WriteLine("Press backspace to return to the menu\n");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.UpArrow)
            {
                selectedFlight--;
                if (selectedFlight == -1) selectedFlight = _flightsPerPage.Count - 1;
            }
            else if (pressedKey == ConsoleKey.DownArrow)
            {
                selectedFlight++;
                if (selectedFlight == _flightsPerPage.Count) selectedFlight = 0;
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                if (_page < _pageAmount)
                {
                    _page++;
                }
                selectedFlight = 0;
                _flightsPerPage = _flightLogic.GetFlightsForPage(_page, 10, _flights);
            }
            else if (pressedKey == ConsoleKey.LeftArrow)
            {
                if (_page > 1)
                {
                    _page--;
                }
                selectedFlight = 0;
                _flightsPerPage = _flightLogic.GetFlightsForPage(_page, 10, _flights);
            }
            else if (pressedKey == ConsoleKey.Backspace)
            {
                MainMenu.Start();
            }
        }

        return _flightsPerPage[selectedFlight].Id;
    }

}