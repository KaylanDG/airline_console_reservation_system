using System;

public static class RemoveFlight
{
    static FlightLogic _flightLogic = new FlightLogic();

    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Do you want to delete one flight or multiple flights? (one/multiple)");
        string answer = Console.ReadLine().ToLower();
        while (answer != "one" && answer != "multiple")
        {
            Console.WriteLine("Invalid input");
            answer = Console.ReadLine().ToLower();
        }

        if (answer.ToLower() == "one")
        {
            DeleteOneFlight();
        }
        else if (answer.ToLower() == "multiple")
        {
            DeleteMultipleFlights();
        }
    }

    private static void DeleteOneFlight()
    {
        FlightOverview.ShowOverview();
        Console.WriteLine("Enter the ID of the flight you want to delete:");
        string flightIdString = Console.ReadLine();

        if (int.TryParse(flightIdString, out int flightId))
        {
            Console.WriteLine($"Are you sure you want to delete flight with ID {flightId}? (y/n)");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                bool removed = FlightLogic.RemoveFlight(flightId);
                if (removed)
                {
                    Console.WriteLine($"Flight with ID {flightId} successfully removed.");
                }
                else
                {
                    Console.WriteLine($"Flight with ID {flightId} not found.");
                }
            }
            else if (confirmation.ToLower() == "n")
            {
                Console.WriteLine("Deletion canceled.");
            }
            else
            {
                Console.WriteLine("Invalid input. Deletion canceled.");
            }
        }
        else
        {
            Console.WriteLine($"Invalid input: {flightIdString}");
        }

        Console.WriteLine("\nPress any key to return to main menu..");
        Console.ReadKey(true);
        MainMenu.Start();
    }

    private static void DeleteMultipleFlights()
    {
        FlightOverview.ShowOverview();
        Console.WriteLine("Enter the ID(s) of the flight(s) you want to delete (comma-separated):");
        string[] flightIds = Console.ReadLine().Split(',');

        foreach (string id in flightIds)
        {
            if (int.TryParse(id, out int flightId))
            {
                bool removed = FlightLogic.RemoveFlight(flightId);
                if (removed)
                {
                    Console.WriteLine($"Flight with ID {flightId} successfully removed.");
                }
                else
                {
                    Console.WriteLine($"Flight with ID {flightId} not found.");
                }
            }
            else
            {
                Console.WriteLine($"Invalid input: {id}");
            }
        }

        Console.WriteLine("\nPress any key to return to main menu..");
        Console.ReadKey(true);
        MainMenu.Start();
    }

}
