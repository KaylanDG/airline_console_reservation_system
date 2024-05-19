public static class EditFlight
{

    static FlightLogic _flightLogic = new FlightLogic();
    static PlaneLogic _planeLogic = new PlaneLogic();

    private static FlightModel flight;
    public static void Start()
    {
        Console.Clear();
        while (flight == default)
        {
            Console.WriteLine("which flight do you want to change (Enter the ID)?");
            int flightID = FlightOverview.SelectFlight();
            flight = _flightLogic.GetById(flightID);
        }
        Console.Clear();

        List<string> options = new List<string>()
        {
            "Change Flight Number",
            "Change From where the flight leaves",
            "Change Destination",
            "Change the Departure Time",
            "Change the Duration of the Flight",
            "Change the Plane",
            "Go back to main menu",
        };

        string prompt = "Choose what you want to edit:";
        Menu menu = new Menu(options, prompt);
        int selectedOption = menu.Run();
        Console.Clear();


        if (selectedOption == 0)
        {
            EditFlight.FlightNumberModify();
        }

        else if (selectedOption == 1)
        {
            EditFlight.FromModify();
        }
        else if (selectedOption == 2)
        {
            EditFlight.DestinationModify();
        }

        else if (selectedOption == 3)
        {
            EditFlight.DepartureTimeModify();
        }
        else if (selectedOption == 4)
        {
            EditFlight.FlightDurationModify();
        }
        else if (selectedOption == 5)
        {
            EditFlight.ChangePlaneModify();
        }

        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
        MainMenu.Start();
    }

    public static void FlightNumberModify()
    {
        Console.WriteLine("What do you want the new flight number to be?");
        string NewFlightNumber = Console.ReadLine();

        while (flight.FlightNumber.Equals(NewFlightNumber))
        {
            Console.WriteLine("That's the same as the current flight number, try again: ");
            NewFlightNumber = Console.ReadLine();
        }

        flight.FlightNumber = NewFlightNumber;
        _flightLogic.UpdateList(flight);
        Console.WriteLine("Succesfully changed the flight number");
    }

    public static void FromModify()
    {
        Console.WriteLine("What do you want the new departure place to be?");
        string NewFrom = Console.ReadLine();

        while (flight.From.Equals(NewFrom))
        {
            Console.WriteLine("That's the same as the current departure place, try again: ");
            NewFrom = Console.ReadLine();
        }

        flight.From = NewFrom;
        _flightLogic.UpdateList(flight);
        Console.WriteLine("Succesfully changed the departure place");
    }

    public static void DestinationModify()
    {
        Console.WriteLine("What do you want the new destination to be?");
        string NewDestination = Console.ReadLine();

        while (flight.Destination.Equals(NewDestination))
        {
            Console.WriteLine("That's the same as the current destination, try again: ");
            NewDestination = Console.ReadLine();
        }

        flight.Destination = NewDestination;
        _flightLogic.UpdateList(flight);
        Console.WriteLine("Succesfully changed the destination");
    }

    public static void DepartureTimeModify()
    {
        Console.WriteLine("What do you want the new departure time to be (dd-MM-yyyy HH:mm)?");
        string NewDepartureTime = Console.ReadLine();


        while (!_flightLogic.IsValidDateFormat(NewDepartureTime) || !_flightLogic.IsValidDate(NewDepartureTime))
        {
            Console.WriteLine("The format is wrong please enter an format like this dd-MM-yyyy HH:mm, try again: ");
            NewDepartureTime = Console.ReadLine();
        }

        while (flight.DepartureTime.Equals(NewDepartureTime))
        {
            Console.WriteLine("That's the same as the current departure time, try again: ");
            NewDepartureTime = Console.ReadLine();
        }


        flight.DepartureTime = NewDepartureTime;
        DateTime departure = DateTime.ParseExact(NewDepartureTime, "dd-MM-yyyy HH:mm", null);
        DateTime arrival = departure.AddMinutes(flight.FlightDuration);
        flight.ArrivalTime = arrival.ToString("dd-MM-yyyy HH:mm");
        _flightLogic.UpdateList(flight);
        Console.WriteLine("Succesfully changed the departure time");
    }

    public static void FlightDurationModify()
    {
        Console.WriteLine("What do you want the new duration to be (in minutes)?");
        int NewFlightDuration = Convert.ToInt32(Console.ReadLine());

        while (flight.FlightDuration.Equals(NewFlightDuration))
        {
            Console.WriteLine("That's the same as the current flight duration, try again: ");
            NewFlightDuration = Convert.ToInt32(Console.ReadLine());
        }

        flight.FlightDuration = NewFlightDuration;
        DateTime departure = DateTime.ParseExact(flight.DepartureTime, "dd-MM-yyyy HH:mm", null);
        DateTime arrival = departure.AddMinutes(NewFlightDuration);
        flight.ArrivalTime = arrival.ToString("dd-MM-yyyy HH:mm");
        _flightLogic.UpdateList(flight);
        Console.WriteLine("Succesfully changed the flight duration");
    }

    public static void ChangePlaneModify()
    {
        _planeLogic.GetPlanes();
        Console.WriteLine("What do you want the new Plane ID to be?");
        int NewPlane = Convert.ToInt32(Console.ReadLine());

        while (flight.FlightDuration.Equals(NewPlane))
        {
            Console.WriteLine("That's the same as the current Plane ID, try again: ");
            NewPlane = Convert.ToInt32(Console.ReadLine());
        }

        flight.Plane = NewPlane;
        _flightLogic.UpdateList(flight);
        Console.WriteLine("Succesfully changed the Plane ID");
    }

}