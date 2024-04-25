using System;
using System.Globalization;

public static class AddFlight
{
    static ReservationLogic _reservationLogic = new ReservationLogic();
    static PlaneLogic _planeLogic = new PlaneLogic();
    static FlightLogic _flightLogic = new FlightLogic();

    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Do you want multiple flights (y/n)?");
        string answer = Console.ReadLine();
        if (answer.ToLower() == "n")
        {
            // Input for a single flight
            // Prompt for flight details
            Console.WriteLine("Enter a flight number:");
            string flightNumber = Console.ReadLine();

            Console.WriteLine("From where?");
            string from = Console.ReadLine();

            Console.WriteLine("Enter a destination:");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter departure time (format: dd-MM-yyyy HH:mm):");
            string departureTimeStr = Console.ReadLine();
            DateTime departureTime = DateTime.ParseExact(departureTimeStr, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter flight duration in minutes:");
            string flightDurationStr = Console.ReadLine();

            // Attempt to parse the flight duration
            int flightDuration;
            while (!int.TryParse(flightDurationStr, out flightDuration))
            {
                Console.WriteLine("Invalid flight duration.");
                return; // Exit the method
            }

            // Check if flight duration is non-negative
            if (flightDuration <= 0)
            {
                Console.WriteLine("Flight duration should be a positive value.");
                return; // Exit the method
            }

            // Prompt for the timezone
            Console.WriteLine("Enter the timezone for the destination:");
            string timezone = Console.ReadLine();

            // Create the flight using FlightLogic
            CreateFlight(flightNumber, from, destination, departureTime, flightDuration, timezone);
        }
        else if (answer.ToLower() == "y")
        {
            // Input for multiple flights
            Console.WriteLine("How many flights do you want to add?");
            int howmany;
            while (!int.TryParse(Console.ReadLine(), out howmany) || howmany <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer:");
            }

            // Prompt for flight details
            Console.WriteLine("Enter details for the first flight:");
            Console.WriteLine("Enter a flight number:");
            string flightNumber = Console.ReadLine();

            Console.WriteLine("From where?");
            string from = Console.ReadLine();

            Console.WriteLine("Enter a destination:");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter departure time (format: dd-MM-yyyy HH:mm):");
            string departureTimeStr = Console.ReadLine();
            DateTime initialDepartureTime = DateTime.ParseExact(departureTimeStr, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter flight duration in minutes:");
            string flightDurationStr = Console.ReadLine();

            // Attempt to parse the flight duration
            int flightDuration;
            while (!int.TryParse(flightDurationStr, out flightDuration))
            {
                Console.WriteLine("Invalid flight duration.");
                return; // Exit the method
            }

            // Check if flight duration is non-negative
            if (flightDuration <= 0)
            {
                Console.WriteLine("Flight duration should be a positive value.");
                return; // Exit the method
            }

            // Prompt for the timezone
            Console.WriteLine("Enter the timezone for the destination:");
            string timezone = Console.ReadLine();

            // Create multiple flights using FlightLogic
            CreateMultipleFlights(howmany, flightNumber, from, destination, initialDepartureTime, flightDuration, timezone);

            Console.WriteLine($"{howmany} flights added successfully!");
        }
        else
        {
            Console.WriteLine("Invalid input");
        }

        Menu.Start();
    }

    private static void CreateFlight(string flightNumber, string from, string destination, DateTime departureTime, int flightDuration, string timezone)
    {
        // Calculate arrival time
        DateTime arrivalTime = departureTime.AddMinutes(flightDuration);

        // Adjust the arrival time based on the timezone
        TimeZoneInfo destTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        arrivalTime = TimeZoneInfo.ConvertTime(arrivalTime, TimeZoneInfo.Utc, destTimeZone);

        // Prompt for plane selection
        Console.WriteLine("Choose a plane:");
        foreach (PlaneModel plane in _planeLogic.GetPlanes())
        {
            Console.WriteLine($"{plane.Id}. {plane.Name}");
        }

        int planeId;
        while (!int.TryParse(Console.ReadLine(), out planeId))
        {
            Console.WriteLine("Invalid input. Please enter a valid plane ID:");
        }

        if (!_flightLogic.IsPlaneAvailable(departureTime, arrivalTime, planeId))
        {
            Console.WriteLine("This plane is not available.");
            Menu.Start();
        }

        // Create the flight using FlightLogic
        FlightModel newFlight = _flightLogic.CreateFlight(
            flightNumber,
            from,
            destination,
            departureTime.ToString("dd-MM-yyyy HH:mm"),
            flightDuration,
            arrivalTime.ToString("dd-MM-yyyy HH:mm"),
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

    private static void CreateMultipleFlights(int howmany, string flightNumber, string from, string destination, DateTime initialDepartureTime, int flightDuration, string timezone)
    {
        // Iterate to create multiple flights
        for (int i = 0; i < howmany; i++)
        {
            // Increment the departure time by one day for each iteration
            DateTime currentDepartureTime = initialDepartureTime.AddDays(i);

            // Create the flight
            CreateFlight(flightNumber, from, destination, currentDepartureTime, flightDuration, timezone);
        }
    }
}
