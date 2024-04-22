public static class ListOfReservations
{
    public static void Start()
    {
        Console.Clear();
        FlightLogic flightlogic = new FlightLogic();
        ReservationLogic Reserv = new ReservationLogic();
        List<ReservationModel> ListReserv = Reserv.ListOfReservationMethod();
        foreach (ReservationModel x in ListReserv)
        {
            string space = "";
            space = new string(' ', 30 - x.ReservationCode.Length);
            FlightModel flight = flightlogic.GetById(x.FlightId);

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

        if (ListReserv.Count == 0)
        {
            Console.WriteLine("You have no reservations.");
        }
        else
        {
            Console.WriteLine("C | Cancel reservation");
            Console.WriteLine("B | Back to menu");
            string choice = Console.ReadLine().ToLower();

            while (choice != "c" && choice != "b")
            {
                Console.WriteLine("C | Cancel reservation");
                Console.WriteLine("B | Back to menu");
                choice = Console.ReadLine().ToLower();
            }

            if (choice == "c")
            {
                Console.WriteLine("Which reservation number to you want to cancel: ");
                string ResCancel = Console.ReadLine();
                bool CancBool = ReservationLogic.RemoveReservation(ResCancel);
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
                        CancBool = ReservationLogic.RemoveReservation(ResCancel);
                    }
                }
                Console.WriteLine($"Reservation with reservation code {ResCancel}");
                Console.WriteLine("Succesfully cancelled.");
            }
        }
    }
}