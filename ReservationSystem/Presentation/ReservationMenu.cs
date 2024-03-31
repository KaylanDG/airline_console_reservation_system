using System;

static class ReservationMenu
{
    static ReservationLogic _reservationLogic = new ReservationLogic();
    static FlightLogic _flightLogic = new FlightLogic();

    // Method to start the reservation process
    public static void Start()
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

            Console.Write("Enter passenger name: ");
            string passengerName = Console.ReadLine();

            Console.WriteLine("Do you want extra luggage (Y/N): ");
            string extraLuggage = Console.ReadLine();

            Reservation reservation = _reservationLogic.CreateReservation(flightID, passengerName);
            if (reservation != null)
            {
                Console.WriteLine("\nReservation successfully created!\n");
                Console.WriteLine($"Your reservation code is: {reservation.ReservationCode}");
            }

            Menu.Start();
        }
        catch (FormatException)
        {
            Console.WriteLine("\nThis flight does not exist!\n");
            Menu.Start();
        }
    }

}
