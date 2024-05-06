public static class AdminReservationOverview
{
    private static int _page = 1;
    private static int _pageSize = 10;
    private static int _selectedReservation;
    private static ReservationLogic _reservationLogic;
    private static FlightLogic _flightLogic;
    private static List<ReservationModel> _reservations;
    public static void Start()
    {
        _selectedReservation = 0;
        _reservationLogic = new ReservationLogic();
        _flightLogic = new FlightLogic();
        _reservations = _reservationLogic.GetReservationsForPage(_page, _pageSize);

        // Get the total page amount
        int pageAmount = (int)Math.Ceiling((double)ReservationAccess.LoadAll().Count / _pageSize);

        ConsoleKey pressedKey = default;
        while (pressedKey != ConsoleKey.C && pressedKey != ConsoleKey.Backspace)
        {
            Console.Clear();
            Console.WriteLine("\nReservation overview");
            Console.WriteLine("Press the upp/down arrow to navigate the overview");
            Console.WriteLine("Press the left/right arrow to switch pages");
            Console.WriteLine("Press the C key to cancel a reservation");
            Console.WriteLine("Press backspace to return to the menu\n");


            Console.WriteLine($"Page: {_page}/{pageAmount}\n");
            ReservationOverview();
            ReservationDetails();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.UpArrow)
            {
                _selectedReservation--;
                if (_selectedReservation == -1) _selectedReservation = _reservations.Count - 1;
            }
            else if (pressedKey == ConsoleKey.DownArrow)
            {
                _selectedReservation++;
                if (_selectedReservation == _reservations.Count) _selectedReservation = 0;
            }
            else if (pressedKey == ConsoleKey.LeftArrow)
            {
                _page--;
                if (_page < 1) _page = 1;
                _reservations = _reservationLogic.GetReservationsForPage(_page, _pageSize);
                _selectedReservation = 0;
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                _page++;
                if (_page > pageAmount) _page = pageAmount;
                _reservations = _reservationLogic.GetReservationsForPage(_page, _pageSize);
                _selectedReservation = 0;
            }
        }

        if (pressedKey == ConsoleKey.Backspace)
        {
            MainMenu.Start();
        }
        else
        {
            CancelReservation();
        }
    }


    private static void ReservationOverview()
    {
        Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,-20} |", "ID", "Reservation Code", "User ID", "Passengers", "Reservation Date");
        Console.WriteLine(new string('-', 89));

        for (int i = 0; i < _reservations.Count; i++)
        {
            ReservationModel reservation = _reservations[i];
            if (i == _selectedReservation)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,-20} |", reservation.Id, reservation.ReservationCode, reservation.UserId, reservation.Passengers.Count, reservation.ReservationDate);
            Console.ResetColor();
        }
        Console.WriteLine(new string('-', 89));
    }

    private static void ReservationDetails()
    {
        ReservationModel reservation = _reservations[_selectedReservation];
        FlightModel flight = _flightLogic.GetById(reservation.FlightId);

        Console.WriteLine();
        Console.WriteLine($"Reservation code:\t{reservation.ReservationCode}");
        Console.WriteLine($"Reservation date:\t{reservation.ReservationDate}");
        Console.WriteLine($"Reservation cost:\t€{reservation.TotalCost}\n");

        Console.WriteLine("Flight\n");
        Console.WriteLine($"Flight number:\t\t{flight.FlightNumber}");
        Console.WriteLine($"From:\t\t\t{flight.From}");
        Console.WriteLine($"Destination:\t\t{flight.Destination}");
        Console.WriteLine($"Departure date:\t\t{flight.DepartureTime}");
        Console.WriteLine($"Arrival date:\t\t{flight.ArrivalTime}\n");

        Console.WriteLine("Passengers\n");
        for (int i = 0; i < reservation.Passengers.Count; i++)
        {
            PassengerModel passenger = reservation.Passengers[i];
            Console.WriteLine($"Passenger {i + 1}:\t\t{passenger.FullName}");
            Console.WriteLine($"Seat:\t\t\t{passenger.SeatNumber} - Price: €{_flightLogic.GetSeatPrice(passenger.SeatNumber, flight)}");
            if (passenger.AdditionalServices.Count > 0)
            {
                Console.WriteLine("Additional Services");
                foreach (ServiceModel service in passenger.AdditionalServices)
                {
                    Console.WriteLine($"- {service.ServiceType} / Amount: {service.Quantity} / Price: €{service.Cost}");
                }
            }
            Console.WriteLine();
        }
    }

    private static void CancelReservation()
    {
        ReservationModel reservation = _reservations[_selectedReservation];
        Console.Clear();
        Console.WriteLine($"Cancel reservation {reservation.ReservationCode}? (yes/no)");
        string cancel = Console.ReadLine().ToLower();
        while (cancel != "yes" && cancel != "no")
        {
            Console.Clear();
            Console.WriteLine("Invalid input, please enter yes or no:");
            cancel = Console.ReadLine();
        }

        Console.Clear();
        if (cancel == "yes")
        {
            if (_reservationLogic.RemoveReservation(reservation.ReservationCode))
            {
                Console.WriteLine("Reservation has been canceled.");
            }
            else
            {
                Console.WriteLine("Could not cancel reservation.");
            }

            Console.WriteLine("\nPress any key to return..");
            Console.ReadKey(true);
        }
        MainMenu.Start();
    }

}