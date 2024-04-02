public static class Reservation
{
    private static FlightLogic _flightLogic = new FlightLogic();
    private static ReservationLogic _reservationLogic = new ReservationLogic();
    private static FlightModel _flight;
    private static List<PassengerModel> _passengers;

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
                Start();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("\nInvalid input!\n");
            Menu.Start();
        }
    }

    public static void SeatOverview()
    {
        List<SeatModel> flightSeats = _flight.GetFlightSeats();


        int seatsPerRow = (_flight.Plane.Name == "Boeing 737") ? 6 : 9;
        int seatRows = flightSeats.Count / seatsPerRow;
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
                            SeatModel seat = flightSeats[seatIndex];

                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("║");

                            if (seat.IsReserved)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
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
        Console.Write(": Seat is reserved.\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Yellow");
        Console.ResetColor();
        Console.Write(": Seat is First Class.\n");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Green");
        Console.ResetColor();
        Console.Write(": Seat is Economy Class.\n");
    }

    public static string SeatSelection()
    {
        SeatOverview();

        Console.WriteLine("\nEnter seat number (example: A-01):");
        string seatNumber = Console.ReadLine().ToUpper();

        if (!_flight.Plane.DoesSeatExist(seatNumber))
        {
            Console.WriteLine("The seat number you entered does not exist.");
            Menu.Start();
        }

        if (_flight.IsSeatReserved(seatNumber))
        {
            Console.WriteLine("The seat number you entered is already reserved.");
            Menu.Start();
        }

        return seatNumber;
    }

    // Method to start the reservation process
    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Enter the amount of passengers:");
        int passengerAmount = 0;
        double totalCost = 0;

        try
        {
            passengerAmount = Convert.ToInt32(Console.ReadLine());
            if (passengerAmount < 0)
            {
                Console.WriteLine("The amount of passengers has to be at least 1.");
                Menu.Start();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("The amount of passengers you entered is not valid.");
            Menu.Start();
        }



        _passengers = new List<PassengerModel>();
        for (int i = 0; i < passengerAmount; i++)
        {
            Console.Clear();
            Console.WriteLine($"\nEnter the full name of passenger {i + 1}:");
            string passengerName = Console.ReadLine();

            Console.WriteLine($"\nSelect a seat for passenger {i + 1}:");
            string passengerSeat = SeatSelection();

            _passengers.Add(new PassengerModel(i + 1, passengerName, passengerSeat));
            totalCost += _flight.GetSeatPrice(passengerSeat);
        }

        ReservationModel reservation = _reservationLogic.CreateReservation(_flight, _passengers, totalCost);
        Console.WriteLine("Your reservation has been booked.");
        Console.WriteLine($"Reservation code: {reservation.ReservationCode}");
    }

}