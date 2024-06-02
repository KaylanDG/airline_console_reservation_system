using System;

public static class RemoveFlight
{
    static FlightLogic _flightLogic = new FlightLogic();

    public static void Start()
    {
        Menu menu = new Menu(new List<string> { "one", "multiple" }, "Do you want to delete one flight or multiple flights? (one/multiple)");

        int answer = menu.Run();

        if (answer == 0)
        {
            DeleteOneFlight();
        }
        else if (answer == 1)
        {
            DeleteMultipleFlights();
        }
    }

    private static void DeleteOneFlight()
    {
        FlightOverview.Flights();
        Console.WriteLine("Enter the ID of the flight you want to delete:");
        string flightIdString = Console.ReadLine();

        if (int.TryParse(flightIdString, out int flightId))
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
            Console.WriteLine($"Invalid input: {flightIdString}");
        }

        Console.WriteLine("\nPress any key to return to main menu..");
        Console.ReadKey(true);
        MainMenu.Start();
    }

    private static void DeleteMultipleFlights()
    {
        FlightOverview.Flights();
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
