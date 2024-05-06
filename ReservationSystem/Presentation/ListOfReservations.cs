public static class ListOfReservations
{
    private static FlightLogic _flightlogic = new FlightLogic();
    private static ReservationLogic _reservationLogic = new ReservationLogic();
    private static List<ReservationModel> _listReserv;

    public static void Start()
    {
        _listReserv = _reservationLogic.ListOfReservationMethod();

        Console.Clear();
        if (_listReserv.Count == 0)
        {
            Console.WriteLine("You have no reservations.");
            Console.WriteLine("\nPress any key to return..");
            Console.ReadKey(true);
            MainMenu.Start();
        }
        else
        {
            List<string> options = new List<string>() { "Cancel reservation", "Back to menu" };
            string prompt = "\nSelect an option from the menu\n";

            Menu menu = new Menu(options, prompt, ReservationOverview);
            int selectedOption = menu.Run();

            switch (selectedOption)
            {
                case 0:
                    CancelReservation();
                    break;
                case 1:
                    Console.Clear();
                    MainMenu.Start();
                    break;
            }

        }
    }

    private static void ReservationOverview()
    {
        foreach (ReservationModel x in _listReserv)
        {
            string space = "";
            space = new string(' ', 30 - x.ReservationCode.Length);
            FlightModel flight = _flightlogic.GetById(x.FlightId);

            // Start of block
            string top = new string('─', 50);
            Console.WriteLine($"┌{top}┐");

            // Reservation code block
            Console.WriteLine("│{0,-50}│", $"Reservation code: {x.ReservationCode}");

            // Reservation date block
            Console.WriteLine("│{0,-50}│", $"Reservation date: {x.ReservationDate}");

            // Total cost block
            Console.WriteLine("│{0,-50}│", $"Total cost: {x.TotalCost}");

            // Passenger(s) block
            Console.WriteLine("│{0,-50}│", "Passenger(s):");
            foreach (PassengerModel passenger in x.Passengers)
            {
                Console.WriteLine("│{0,-50}│", $"   - {passenger.FullName} ");
                Console.WriteLine("│{0,-50}│", $"     SeatNumber: {passenger.SeatNumber} ");
            }

            // Flight number block
            Console.WriteLine("│{0,-50}│", $"  Flight number: {flight.FlightNumber} ");

            // From block
            Console.WriteLine("│{0,-50}│", $"  From: {flight.From} ");

            // Destination block
            Console.WriteLine("│{0,-50}│", $"  Destination: {flight.Destination} ");

            // Departure time block
            Console.WriteLine("│{0,-50}│", $"  Departure time: {flight.DepartureTime}");

            // Arrival time block
            Console.WriteLine("│{0,-50}│", $"  Arrival time: {flight.ArrivalTime}");

            // End of block
            Console.WriteLine($"└{top}┘");
        }
    }

    private static void CancelReservation()
    {
        Console.WriteLine("\nWhich reservation number to you want to cancel: ");
        string ResCancel = Console.ReadLine();
        bool CancBool = _reservationLogic.RemoveReservation(ResCancel);
        if (CancBool == false)
        {
            // No reservation code gevonden.
            // Loop door totdat een code is gevonden met die reservatie code
            // (Geen cancel knop in deze functie)

            while (CancBool == false)
            {
                Console.WriteLine("\nThat's no reservation code.");
                Console.WriteLine("Can you give a valid reservation code");
                Console.WriteLine("Reservation code: ");
                ResCancel = Console.ReadLine();
                CancBool = _reservationLogic.RemoveReservation(ResCancel);
            }
        }
        Console.WriteLine($"Reservation with reservation code {ResCancel}");
        Console.WriteLine("Succesfully cancelled.");
        Console.WriteLine("\nPress any key to return..");

        Console.ReadKey(true);
        MainMenu.Start();
    }
}