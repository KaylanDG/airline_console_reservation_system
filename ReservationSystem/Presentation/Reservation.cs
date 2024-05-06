public static class Reservation
{
    private static FlightLogic _flightLogic;
    private static ReservationLogic _reservationLogic;
    private static PlaneLogic _planeLogic;
    private static ReservationModel _reservation;
    private static FlightModel _flight;
    private static List<SeatModel> _flightSeats;

    public static void Start()
    {
        _flightLogic = new FlightLogic();
        _reservationLogic = new ReservationLogic();
        _planeLogic = new PlaneLogic();

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

    public static void ReservationProcess()
    {
        int passengerAmount = PassengerAmount();
        _reservation = new ReservationModel
        {
            FlightId = _flight.Id,
            UserId = AccountsLogic.CurrentAccount!.Id
        };

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


            Console.WriteLine($"\nSelect a seat for passenger {i + 1}\n");
            passenger.SeatNumber = SeatSelection();

            ServiceModel extraLuggage = ExtraLuggage();
            if (extraLuggage != null)
            {
                passenger.AdditionalServices.Add(extraLuggage);
            }

            _reservation.Passengers.Add(passenger);
            _reservation.TotalCost += _flightLogic.GetSeatPrice(passenger.SeatNumber, _flight);
            _flightSeats = _flightLogic.UpdateFlightSeats(passenger.SeatNumber, _flightSeats);
        }

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
        Console.WriteLine($"\nYour reservation\n");
        Console.WriteLine($"From:\t\t{_flight.From}");
        Console.WriteLine($"Destination:\t{_flight.Destination}");
        Console.WriteLine($"Departure:\t{_flight.DepartureTime}");
        Console.WriteLine("\nPassengers:\n");
        for (int i = 1; i <= _reservation.Passengers.Count; i++)
        {
            PassengerModel passenger = _reservation.Passengers[i - 1];
            Console.WriteLine($" Passenger {i}:\t{passenger.FullName}");
            Console.WriteLine($" Seat:\t\t{passenger.SeatNumber} - Price: €{_flightLogic.GetSeatPrice(passenger.SeatNumber, _flight)}");
            if (passenger.AdditionalServices.Count > 0)
            {
                Console.WriteLine(" Additional Services");
                foreach (ServiceModel service in passenger.AdditionalServices)
                {
                    Console.WriteLine($" - {service.ServiceType} / Amount: {service.Quantity} / Price: €{service.Cost}");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', 30));
        Console.WriteLine($"Total price: €{_reservation.TotalCost}");
        Console.WriteLine($"Book reservation: (Y/N)");

        string bookResrevation = Console.ReadLine().ToLower();
        while (bookResrevation != "y" && bookResrevation != "n")
        {
            bookResrevation = Console.ReadLine().ToLower();
        }

        Console.Clear();

        if (bookResrevation == "y")
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
        Console.WriteLine("\nDo you want extra luggage (Y/N): ");
        string extraLuggage = Console.ReadLine().ToLower();

        while (extraLuggage != "y" && extraLuggage != "n")
        {
            Console.WriteLine("Wrong input.");
            Console.WriteLine("Do you want extra luggage (Y/N): ");
            extraLuggage = Console.ReadLine().ToLower();
        }
        if (extraLuggage == "y")
        {
            Console.Clear();
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