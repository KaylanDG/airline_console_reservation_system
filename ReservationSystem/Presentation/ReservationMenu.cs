using System;

static class ReservationMenu
{
    static ReservationLogic _reservationLogic = new ReservationLogic();
    static FlightLogic _flightLogic = new FlightLogic();

    // Method to start the reservation process
    public static void Start()
    {
        Console.Write("\nEnter flight number: ");
        string flightNumber = Console.ReadLine();

        if (!_reservationLogic.DoesFlightExist(flightNumber))
        {
            Console.WriteLine("\nThe flight number you entered does not exist!\n");
            Menu.Start();
        }

        Console.Write("Enter passenger name: ");
        string passengerName = Console.ReadLine();

        _reservationLogic.CreateReservation(flightNumber, passengerName);

        Console.WriteLine("\nReservation successfully created!\n");
        Menu.Start();
    }

}
