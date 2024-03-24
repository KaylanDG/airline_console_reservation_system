using System;

static class Reservation
{
    static ReservationLogic _reservationLogic = new ReservationLogic();

    // Method to start the reservation process
    public static void MakeReservation()
    {
        Console.Clear();
        Console.Write("Enter flight number: ");
        string flightNumber = Console.ReadLine();
        Console.Write("Enter passenger name: ");
        string passengerName = Console.ReadLine();

        // Create a new reservation model
        var reservation = new ReservationModel
        {
            Id = GenerateReservationId(),
            ReservationCode = GenerateRandomReservationCode(),
            ReservationDate = DateTime.Now.ToString(),
            FlightNumber = flightNumber,
            // Assuming only one passenger for simplicity
            Passengers = new List<PassengerModel>
            {
                new PassengerModel { FullName = passengerName }
            }
        };

        // Add the reservation using ReservationLogic
        _reservationLogic.UpdateList(reservation);

        Console.WriteLine("Reservation successfully created!");
    }


    static string GenerateRandomReservationCode()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, 8)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static int GenerateReservationId()
    {
        // Create an instance of ReservationAccess
        var reservationAccess = new ReservationAccess();

        // Load existing reservations from the JSON file
        var reservations = ReservationAccess.LoadAll();

        // Find the highest existing ID
        int maxId = reservations.Max(r => r.Id);

        // Increment the highest existing ID by one to generate a new ID
        return maxId + 1;
    }

}
