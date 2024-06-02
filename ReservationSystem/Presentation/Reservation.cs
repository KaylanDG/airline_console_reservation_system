public static class Reservation
{
    private static FlightLogic _flightLogic = new FlightLogic();
    private static ReservationLogic _reservationLogic = new ReservationLogic();
    private static PlaneLogic _planeLogic = new PlaneLogic();
    private static DiscountLogic _discountLogic = new DiscountLogic();
    private static ReservationModel _reservation;
    private static FlightModel _flight;
    private static List<SeatModel> _flightSeats;
    private static List<PassengerModel> _passengers;
    private static bool _dicount = false;

    public static void Start()
    {

        if (_flightLogic.GetAvailableFlights().Count <= 0)
        {
            Console.Clear();
            Console.WriteLine("\nThere are currently no flights available.");
            Console.WriteLine("\nPress any key to return to main menu..");
            Console.ReadKey(true);
            MainMenu.Start();
        }
        else
        {
            int flightID = FlightOverview.SelectFlight();

            if (flightID > 0)
            {
                _flight = _flightLogic.GetById(flightID);
                _flightSeats = _flightLogic.GetFlightSeats(_flight);

                ReservationProcess();
                ReservationMenu();
                MainMenu.Start();
            }
        }
    }

    public static void ReservationProcess()
    {
        if (_passengers == null)
        {
            int passengerAmount = PassengerAmount();
            _passengers = new List<PassengerModel>();

            for (int i = 0; i < passengerAmount; i++)
            {
                PassengerModel passenger = new PassengerModel(i + 1);
                Console.Clear();
                Console.WriteLine($"\nEnter the full name of passenger {i + 1}:");
                string passengerName = Console.ReadLine();
                while (passengerName == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter your name.\n");
                    Console.ResetColor();
                    passengerName = Console.ReadLine();
                }
                passenger.FullName = passengerName!;
                _passengers.Add(passenger);
            }
        }

        _reservation = new ReservationModel
        {
            FlightId = _flight.Id,
            UserId = AccountsLogic.CurrentAccount!.Id
        };

        foreach (PassengerModel passenger in _passengers)
        {

            Console.WriteLine($"\nSelect a seat for {passenger.FullName}\n");
            passenger.SeatNumber = SeatSelection();

            ServiceModel extraLuggage = ExtraLuggage();
            if (extraLuggage != null)
            {
                passenger.AdditionalServices.Add(extraLuggage);
            }

            _reservation.TotalCost += _flightLogic.GetSeatPrice(passenger.SeatNumber, _flight);
            _flightSeats = _flightLogic.UpdateFlightSeats(passenger.SeatNumber, _flightSeats);
        }

        _reservation.Passengers = _passengers;
    }


    public static int PassengerAmount()
    {
        int passengerAmount = 0;
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("\nEnter the amount of passengers:");
            try
            {
                PlaneModel plane = _flightLogic.GetPlaneByID(_flight.Plane);
                passengerAmount = Convert.ToInt32(Console.ReadLine());
                if (passengerAmount < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe amount of passengers has to be at least 1.\n");
                    Console.ResetColor();
                }
                else if (passengerAmount > plane.EconomySeats + plane.FirstClassSeats)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe amount of passengers has exceeded the maximum amount.\n");
                    Console.ResetColor();
                }
                else
                {
                    validInput = true;
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe amount of passengers you entered is not valid.\n");
                Console.ResetColor();
            }
        }

        return passengerAmount;
    }

    public static string SeatSelection()
    {
        SeatOverview();
        bool validSeat = false;
        string seatNumber = "";

        while (!validSeat)
        {
            Console.WriteLine("\nEnter seat number (example: A-01):");
            seatNumber = Console.ReadLine().ToUpper();
            if (!_planeLogic.DoesSeatExist(seatNumber, _flight.Plane))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe seat number you entered does not exist.\n");
                Console.ResetColor();
            }

            else if (_flightLogic.IsSeatReserved(seatNumber, _flightSeats))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe seat number you entered is already reserved.\n");
                Console.ResetColor();
            }

            else if (_flightLogic.IsSeatSelected(seatNumber, _flightSeats))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe seat number you entered is already selected.\n");
                Console.ResetColor();
            }
            else
            {
                validSeat = true;
            }
        }


        return seatNumber;
    }

    public static string SeatSelection(int flightID)
    {
        _flight = _flightLogic.GetById(flightID);
        _flightSeats = _flightLogic.GetFlightSeats(_flight);

        SeatOverview();
        bool validSeat = false;
        string seatNumber = "";

        while (!validSeat)
        {
            Console.WriteLine("It will cost an aditional 25 euros to change your seat.");
            Console.WriteLine("\nEnter seat number (example: A-01)");
            Console.WriteLine("Or press enter if you wanna remain in your current seat: ");
            seatNumber = Console.ReadLine().ToUpper();
            if (seatNumber == "")
            {
                UserReservationOverview.Start();
            }
            else if (!_planeLogic.DoesSeatExist(seatNumber, _flight.Plane))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe seat number you entered does not exist.\n");
                Console.ResetColor();
            }

            else if (_flightLogic.IsSeatReserved(seatNumber, _flightSeats))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe seat number you entered is already reserved.\n");
                Console.ResetColor();
            }

            else if (_flightLogic.IsSeatSelected(seatNumber, _flightSeats))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe seat number you entered is already selected.\n");
                Console.ResetColor();
            }
            else
            {
                validSeat = true;
            }
        }


        return seatNumber;
    }


    public static void SeatOverview()
    {
        int seatsPerRow = (_flightLogic.GetPlaneByID(_flight.Plane).Name == "Boeing 737") ? 6 : 9;
        int seatRows = _flightSeats.Count / seatsPerRow;
        int seatIndex = 0;

        for (int i = 0; i < seatRows; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < seatsPerRow + 2; k++)
                {
                    if (k == 3 || k == 7)
                    {
                        Console.Write("        ");
                    }
                    else
                    {
                        if (j == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("╔════╗");
                        }
                        else if (j == 1)
                        {
                            SeatModel seat = _flightSeats[seatIndex];

                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("║");

                            if (seat.IsReserved)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (seat.IsSelected)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else if (seat.SeatType == "First Class")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }

                            Console.Write($"{seat.SeatNumber}");

                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("║");

                            seatIndex++;
                        }
                        else if (j == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("╚════╝");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        SeatInfo();
        Console.ResetColor();
    }

    public static void SeatInfo()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Red");
        Console.ResetColor();
        Console.Write(": Seat is already reserved.\n");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Blue");
        Console.ResetColor();
        Console.Write(": Seat is alredy selected.\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Yellow");
        Console.ResetColor();
        Console.Write(": Seat is First Class.\n");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Green");
        Console.ResetColor();
        Console.Write(": Seat is Economy Class.\n");

    }

    public static void CompleteReservation(bool isReturnFlight = false)
    {
        int bookResrevation = -1;

        while (bookResrevation != 0 && bookResrevation != 1)
        {
            string reservationPrice = "";
            reservationPrice += "\nYour reservation\n";
            reservationPrice += $"From:\t\t{_flight.From}\n";
            reservationPrice += $"Destination:\t{_flight.Destination}\n";
            reservationPrice += $"Departure:\t{_flight.DepartureTime}\n";
            reservationPrice += "\nPassengers:\n";
            for (int i = 1; i <= _reservation.Passengers.Count; i++)
            {
                PassengerModel passenger = _reservation.Passengers[i - 1];
                reservationPrice += $" Passenger {i}:\t{passenger.FullName}\n";
                reservationPrice += $" Seat:\t\t{passenger.SeatNumber} - Price: €{_flightLogic.GetSeatPrice(passenger.SeatNumber, _flight)}\n";
                if (passenger.AdditionalServices.Count > 0)
                {
                    reservationPrice += " Additional Services\n";
                    foreach (ServiceModel service in passenger.AdditionalServices)
                    {
                        reservationPrice += $" - {service.ServiceType} / Amount: {service.Quantity} / Price: €{service.Cost}\n";
                    }
                }
            }
            reservationPrice += "\n";
            reservationPrice += new string('-', 30);
            reservationPrice += $"\nTotal price: €{_reservation.TotalCost}\n";

            List<string> options = new List<string> { "Book reservation", "Cancel reservation" };
            if (!_dicount) options.Add("Apply discount");

            Menu bookReservationMenu = new Menu(options, reservationPrice);
            bookResrevation = bookReservationMenu.Run();

            if (bookResrevation == 2)
            {
                Console.WriteLine("Enter a discount code:");
                string discountCode = Console.ReadLine();
                if (_discountLogic.DoesCodeExist(discountCode))
                {
                    DiscountModel discount = _discountLogic.GetDiscount(discountCode);
                    if (_discountLogic.IsCodeValid(discount))
                    {
                        _reservation.TotalCost -= _discountLogic.GetDiscountPrice(discount, _reservation.TotalCost);
                        Console.WriteLine("Discount applied");
                        _dicount = true;
                    }
                    else
                    {
                        Console.WriteLine("This discount is no longer valid");
                    }
                }
                else
                {
                    Console.WriteLine("This code does not exist");
                }

                Console.WriteLine("\nPress any key to return..");
                Console.ReadKey(true);
            }
        }

        Console.Clear();

        if (bookResrevation == 0)
        {
            string reservationCode = _reservationLogic.CompleteReservation(_reservation);

            if (!isReturnFlight)
            {
                List<string> options = new List<string>() { "Book return flight", "Return to main menu" };
                string prompt = $"\nYour reservation has been booked.\nReservation Code: {reservationCode}\n";

                Menu menu = new Menu(options, prompt);
                int selectedOption = menu.Run();

                switch (selectedOption)
                {
                    case 0:
                        ReturnFlight();
                        break;
                    case 1:
                        MainMenu.Start();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"\nYour reservation has been booked.\nReservation Code: {reservationCode}\n");
                Console.WriteLine("\nPress any key to return..");
                Console.ReadKey(true);
                MainMenu.Start();
            }

        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYour reservation has been Canceled.\n");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to return..");
            Console.ReadKey(true);
            MainMenu.Start();
        }

    }

    public static void ReturnFlight()
    {
        List<FlightModel> returnFlights = _flightLogic.GetReturnFlights(_flight);
        if (returnFlights.Count > 0)
        {
            FlightOverview._flights = returnFlights;
            int flightID = FlightOverview.SelectFlight();

            if (flightID > 0)
            {
                _flight = _flightLogic.GetById(flightID);
                _flightSeats = _flightLogic.GetFlightSeats(_flight);

                bool Continue = false;
                while (!Continue)
                {
                    string passengerInfo = "Passengers:\n";

                    for (int i = 1; i <= _passengers.Count; i++)
                    {
                        PassengerModel passenger = _passengers[i - 1];
                        passengerInfo += $"Passenger {i}: {passenger.FullName}\n";
                    }

                    Menu menu = new Menu(new List<string> { "Add passenger", "Remove passenger", "Continue" }, passengerInfo);
                    int menuOption = menu.Run();

                    if (menuOption == 0)
                    {
                        PassengerModel passenger = new PassengerModel(_passengers.Count - 1);
                        Console.WriteLine("\nEnter the fullname of the new passenger:");
                        string fullName = Console.ReadLine();
                        while (fullName == "")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nPlease enter your name.\n");
                            Console.ResetColor();
                            fullName = Console.ReadLine();
                        }
                        passenger.FullName = fullName;
                        _passengers.Add(passenger);

                    }
                    else if (menuOption == 1)
                    {
                        if (_passengers.Count == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("\nYou cant delete all Passengers.");
                            Console.WriteLine("\nPress any key to return..");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            List<string> names = new List<string>();
                            foreach (PassengerModel p in _passengers) names.Add($" {p.FullName}");

                            Menu removePassenger = new Menu(names, "Choose which passenger you want to remove.");
                            int passengerIndex = removePassenger.Run();
                            _passengers.RemoveAt(passengerIndex);
                        }
                    }
                    else if (menuOption == 2)
                    {
                        Continue = true;
                    }
                }

                ReservationProcess();
                ReservationMenu(true);
            }
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThere are no return flights for this destination.\n");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to return..");
            Console.ReadKey(true);
            MainMenu.Start();
        }
    }

    public static void ReservationMenu(bool isReturnFlight = false)
    {

        List<string> options = new List<string>() { "Book reservation", "Cancel reservation" };
        string prompt = "\nSelect an option from the menu\n";

        Menu menu = new Menu(options, prompt);
        int selectedOption = menu.Run();

        switch (selectedOption)
        {
            case 0:
                CompleteReservation(isReturnFlight);
                break;
            case 1:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYour reservation has been Canceled.\n");
                Console.ResetColor();

                Console.WriteLine("\nPress any key to return..");
                Console.ReadKey(true);
                MainMenu.Start();
                break;
        }
    }

    public static ServiceModel ExtraLuggage()
    {
        Menu extraLuggageMenu = new Menu(new List<string> { "yes", "no" }, "Do you want extra luggage:");
        int extraLuggage = extraLuggageMenu.Run();

        if (extraLuggage == 0)
        {
            Console.Clear();
            int maxLuggageAmount = _flightLogic.MaxLuggageAmount(_flight.Id);
            int extraLuggageAmount = 0;
            ConsoleKey pressedKey = default;

            while (pressedKey != ConsoleKey.Enter)
            {
                Console.WriteLine("\nAmount of extra luggage (1 = 20kg):");
                Console.WriteLine($"<{extraLuggageAmount}>");
                Console.WriteLine($"Total price: €{_reservationLogic.ExtraLuggagePrice(extraLuggageAmount)}");
                Console.WriteLine("\nUse the up/down arrow to add or remove, press enter to confirm\n");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                pressedKey = keyInfo.Key;

                if (pressedKey == ConsoleKey.UpArrow)
                {
                    extraLuggageAmount++;
                    if (extraLuggageAmount > maxLuggageAmount) extraLuggageAmount = maxLuggageAmount;
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    extraLuggageAmount--;
                    if (extraLuggageAmount < 0) extraLuggageAmount = 0;
                }
                Console.Clear();
            }

            double price = _reservationLogic.ExtraLuggagePrice(extraLuggageAmount);
            _reservation.TotalCost += price;
            return new ServiceModel("Extra Luggage", extraLuggageAmount, price);

        }
        return null;
    }

}