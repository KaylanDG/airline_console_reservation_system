public static class Reservation
{
    private static FlightLogic _flightLogic = new FlightLogic();
    private static ReservationLogic _reservationLogic = new ReservationLogic();
    private static FlightModel _flight;
    private static List<SeatModel> _flightSeats;

    public static void Start()
    {
        SelectFlight();
        int passengerAmount = EnterPassengerAmount();

        List<PassengerModel> passengers = new List<PassengerModel>();
        double totalCost = 0.0;

        for (int i = 0; i < passengerAmount; i++)
        {
            Console.Clear();
            Console.WriteLine($"\nEnter the full name of passenger {i + 1}:");
            string passengerName = Console.ReadLine();

            Console.WriteLine($"\nSelect a seat for passenger {i + 1}\n");
            string passengerSeat = SeatSelection();

            passengers.Add(new PassengerModel(i + 1, passengerName, passengerSeat));
            _flightSeats = _flightLogic.UpdateFlightSeats(passengerSeat, _flightSeats);
            totalCost += _flightLogic.GetSeatPrice(passengerSeat, _flightSeats, _flight);
        }

        Console.Clear();
        ReservationModel reservation = _reservationLogic.CreateReservation(_flight, passengers, totalCost);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nYour reservation has been booked.\n");
        Console.ResetColor();
        Console.WriteLine($"Reservation code: {reservation.ReservationCode}\n");
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

    public static int EnterPassengerAmount()
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

            if (!_flight.Plane.DoesSeatExist(seatNumber))
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
        int seatsPerRow = (_flight.Plane.Name == "Boeing 737") ? 6 : 9;
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

}