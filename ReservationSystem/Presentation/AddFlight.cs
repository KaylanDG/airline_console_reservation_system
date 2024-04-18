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
            Console.WriteLine("Enter a flight number:");
            string flightNumber = Console.ReadLine();

            Console.WriteLine("From where?");
            string from = Console.ReadLine();

            Console.WriteLine("Enter a destination:");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter departure time (format: dd-MM-yyyy HH:mm tt):");
            string departureTimeStr = Console.ReadLine();
            DateTime departureTime = DateTime.ParseExact(departureTimeStr, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture);

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

            // Calculate arrival time
            DateTime arrivalTime = departureTime.AddMinutes(flightDuration);

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
                return; // Exit the method
            }


            // Create the flight using FlightLogic
            FlightModel newFlight = _flightLogic.CreateFlight(
                flightNumber,
                from,
                destination,
                departureTime.ToString("dd-MM-yyyy HH:mm tt"),
                flightDuration,
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
        else if (answer.ToLower() == "y")
        {
            Console.WriteLine("how many flights do you want to add");
            int howmany;
            while (!int.TryParse(Console.ReadLine(), out howmany) || howmany <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer:");
            }

            Console.WriteLine("Enter a flight number:");
            string flightNumber = Console.ReadLine();

            Console.WriteLine("From where?");
            string from = Console.ReadLine();

            Console.WriteLine("Enter a destination:");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter departure time (format: dd-MM-yyyy HH:mm tt):");
            string departureTimeStr = Console.ReadLine();
            DateTime departureTime = DateTime.ParseExact(departureTimeStr, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture);

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

            // Calculate arrival time
            DateTime arrivalTime = departureTime.AddMinutes(flightDuration);

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

            if (!_flightLogic.IsPlaneAvailable(departureTime, arrivalTime, planeId, howmany))
            {
                return; // Exit the method
            }

            // Create multiple flights using FlightLogic
            _flightLogic.CreateMultipleFlights(
                howmany,
                flightNumber,
                from,
                destination,
                departureTime.ToString("dd-MM-yyyy HH:mm tt"),
                flightDuration,
                arrivalTime.ToString("dd-MM-yyyy HH:mm tt"),
                planeId
            );

            Console.WriteLine($"{howmany} flights added successfully!");
        }
        else
        {
            Console.WriteLine("invalid input");
        }

        Menu.Start();
    }
}
