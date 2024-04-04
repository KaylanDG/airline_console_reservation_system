public static class AddFlight
{
    static ReservationLogic _reservationLogic = new ReservationLogic();
    static FlightLogic _flightLogic = new FlightLogic();

    public static void Start()
    {
        Console.WriteLine("Enter a flight number:");
        string flightNumber = Console.ReadLine();

        Console.WriteLine("From where?");
        string from = Console.ReadLine();

        Console.WriteLine("Enter a destination:");
        string destination = Console.ReadLine();

        Console.WriteLine("Enter departure time (format: dd-MM-yyyy HH:mm tt):");
        string departureTimeStr = Console.ReadLine();
        DateTime departureTime = DateTime.ParseExact(departureTimeStr, "dd-MM-yyyy HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);

        Console.WriteLine("Enter flight duration (format: HH:mm):");
        string flightDurationStr = Console.ReadLine();
        TimeSpan flightDuration = TimeSpan.ParseExact(flightDurationStr, "hh\\:mm", System.Globalization.CultureInfo.InvariantCulture);

        // Calculate arrival time
        DateTime arrivalTime = departureTime.Add(flightDuration);

        Console.WriteLine("Enter plane ID:");
        int planeId = Convert.ToInt32(Console.ReadLine());

        // Create the flight using FlightLogic
        Flight newFlight = _flightLogic.CreateFlight(
            flightNumber,
            from,
            destination,
            departureTime.ToString("dd-MM-yyyy HH:mm tt"),
            flightDuration.ToString(),
            arrivalTime.ToString("dd-MM-yyyy HH:mm tt"),
            planeId
        );

        if (newFlight != null)
        {
            Console.WriteLine("Flight added successfully!");
            Console.WriteLine($"Flight ID: {newFlight.Id}");
        }
        else
        {
            Console.WriteLine("Failed to add flight.");
        }
    }
}


//    Flight newFlight = new Flight(
//         GenerateFlightId(),
//         flightnumber,
//         from,
//         destination,
//         departure_time,
//         flight_duration,
//         arrival_time,
//         GetPlaneByID(id)
//     );