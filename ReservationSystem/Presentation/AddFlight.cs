using System;
using System.Globalization;

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
        DateTime departureTime = DateTime.ParseExact(departureTimeStr, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture);

        Console.WriteLine("Enter flight duration (format: HH:mm):");
        string flightDurationStr = Console.ReadLine().Trim(); // Trim whitespace

        // Attempt to parse the flight duration
        if (!TimeSpan.TryParseExact(flightDurationStr, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan flightDuration))
        {
            Console.WriteLine("Invalid flight duration format. Please enter it as HH:mm (e.g., 01:30).");
            return; // Exit the method
        }

        // Check if flight duration is non-negative
        if (flightDuration.TotalMinutes <= 0)
        {
            Console.WriteLine("Flight duration should be a positive value.");
            return; // Exit the method
        }

        // Calculate arrival time
        DateTime arrivalTime = departureTime.Add(flightDuration);

        Console.WriteLine("Enter plane ID:");
        int planeId;
        while (!int.TryParse(Console.ReadLine(), out planeId))
        {
            Console.WriteLine("Invalid input. Please enter a valid plane ID:");
        }

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
