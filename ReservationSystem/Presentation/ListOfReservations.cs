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
            Console.WriteLine("│{0,-50}│", $"Reservation code: {x.ReservationDate}");

            // Reservation date block
            Console.WriteLine("│{0,-50}│", $"Reservation date: {x.ReservationDate}");

            // Total cost block
            Console.WriteLine("│{0,-50}│", $"  Total cost: {x.TotalCost}");

            // Passenger(s) block
            Console.WriteLine("│{0,-50}│", "  Passenger(s):");
            foreach (PassengerModel passenger in x.Passengers)
            {
                Console.WriteLine("│{0,-50}│", $"     - {passenger.FullName} ");
                Console.WriteLine("│{0,-50}│", $"       SeatNumber: {passenger.SeatNumber} ");
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
        if (ListReserv == null)
        {
            Console.WriteLine("You have no reservations.");
        }
    }
}