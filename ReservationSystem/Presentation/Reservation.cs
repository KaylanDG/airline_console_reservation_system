public static class Reservation
{
    private static FlightLogic _flightLogic;
    private static ReservationLogic _reservationLogic;
    private static ReservationModel _reservation;
    private static FlightModel _flight;
    private static List<SeatModel> _flightSeats;

    public static void Start()
    {
        _flightLogic = new FlightLogic();
        _reservationLogic = new ReservationLogic();
        SelectFlight();
        ReservationProcess();
        ReservationMenu();
        Menu.Start();
    }

    public static void SelectFlight()
    {
        try
        {
            Console.Write("\nEnter the index number of the flight you wish to book: ");
            int flightID = Convert.ToInt32(Console.ReadLine());

            if (!_flightLogic.DoesFlightExist(flightID))
            {
                Console.WriteLine("\nThis flight does not exist!\n");
                Menu.Start();
            }
            else
            {
                _flight = _flightLogic.GetById(flightID);
                _flightSeats = _flightLogic.GetFlightSeats(_flight);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("\nInvalid input!\n");
            Menu.Start();
        }
    }

    public static void ReservationProcess()
    {
        int passengerAmount = PassengerAmount();
        _reservation = new ReservationModel();

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

        _reservation.FlightId = _flight.Id;
        _reservation.UserId = AccountsLogic.CurrentAccount!.Id;
        _reservationLogic.SaveReservation(_reservation);
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
                passengerAmount = Convert.ToInt32(Console.ReadLine());
                if (passengerAmount < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe amount of passengers has to be at least 1.\n");
                    Console.ResetColor();
                }
                else if (passengerAmount > _flightLogic.GetPlaneByID(_flight.Id).EconomySeats + _flightLogic.GetPlaneByID(_flight.Id).FirstClassSeats)
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
            if (!_flightLogic.GetPlaneByID(_flight.Plane).DoesSeatExist(seatNumber))
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

    public static void CompleteReservation()
    {
        Console.WriteLine($"\nYour {((_reservationLogic.SavedReservations.Count > 1) ? "reservations" : "reservation")}:");
        foreach (ReservationModel reservation in _reservationLogic.SavedReservations)
        {
            FlightModel flight = _flightLogic.GetById(reservation.FlightId);
            Console.WriteLine($"From:\t\t{flight.From}");
            Console.WriteLine($"Destination:\t{flight.Destination}");
            Console.WriteLine($"Departure:\t{flight.DepartureTime}");
            for (int i = 1; i <= reservation.Passengers.Count; i++)
            {
                Console.WriteLine($"Passenger {i}:\t{reservation.Passengers[i - 1].FullName} - Seat {reservation.Passengers[i - 1].SeatNumber}");
            }
            Console.WriteLine($"Reservation price: €{reservation.TotalCost}");
            Console.WriteLine();
        }

        Console.WriteLine(new string('-', 30));
        Console.WriteLine($"Total price: €{_reservationLogic.GetTotalCost()}");
        Console.WriteLine($"Book {((_reservationLogic.SavedReservations.Count > 1) ? "reservations" : "reservation")}: (Y/N)");

        string bookResrevation = Console.ReadLine().ToLower();
        while (bookResrevation != "y" && bookResrevation != "n")
        {
            bookResrevation = Console.ReadLine().ToLower();
        }

        Console.Clear();

        if (bookResrevation == "y")
        {
            _reservationLogic.CompleteReservation();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYour reservation has been booked.\n");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYour reservation has been Canceled.\n");
            Console.ResetColor();
        }

        Menu.Start();
    }

    public static void ReturnFlight()
    {
        List<FlightModel> returnFlights = _flightLogic.GetReturnFlights(_flight);
        if (returnFlights.Count > 0)
        {
            FlightOverview.ShowOverview(returnFlights);
            SelectFlight();
            ReservationProcess();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no return flights for this destination.");
            Console.ResetColor();
        }
    }

    public static void ReservationMenu()
    {


        while (true)
        {
            Console.Clear();
            if (_reservationLogic.SavedReservations.Count < 2)
            {
                Console.WriteLine("R | Book return flight");
            }
            Console.WriteLine($"B | Book {((_reservationLogic.SavedReservations.Count > 1) ? "reservations" : "reservation")}");
            Console.WriteLine("C | Cancel");
            string menuOption = Console.ReadLine().ToLower();
            while (menuOption != "r" && menuOption != "c" && menuOption != "b")
            {
                menuOption = Console.ReadLine();
            }

            if (menuOption == "r" && _reservationLogic.SavedReservations.Count < 2)
            {
                ReturnFlight();
            }
            else if (menuOption == "b")
            {
                CompleteReservation();
                break;
            }
            else if (menuOption == "c")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYour reservation has been Canceled.\n");
                Console.ResetColor();
                break;
            }
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
            int extraLuggageAmount = 0;
            bool LoopBool = true;
            while (LoopBool)
            {
                try
                {
                    Console.WriteLine("How much extra Luggage do you want: ");
                    extraLuggageAmount = Convert.ToInt32(Console.ReadLine());
                    LoopBool = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("That's not a number!");
                }
            }

            double price = _reservationLogic.ExtraLuggagePrice(extraLuggageAmount);

            _reservation.TotalCost += price;
            return new ServiceModel("Extra Luggage", extraLuggageAmount, price);

        }
        return null;
    }

}