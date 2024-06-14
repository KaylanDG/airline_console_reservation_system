public static class AddFlight
{
    static PlaneLogic _planeLogic = new PlaneLogic();
    static FlightLogic _flightLogic = new FlightLogic();

    public static void Start()
    {
        List<PlaneModel> planes = _planeLogic.GetPlanes();

        Console.Clear();
        Console.WriteLine("Enter a flight number:");
        string flightNumber = Console.ReadLine();
        while (!_flightLogic.IsValidFlightNumber(flightNumber))
        {
            Console.WriteLine("The flight number you entered is not valid, please try again");
            Console.WriteLine("The flight number should start with 2 letters followed by 4 numbers");
            flightNumber = Console.ReadLine();
        }


        Console.WriteLine("\nChoose which plane you want to use:");
        bool validPlane = false;
        int planeId = 0;
        while (!validPlane)
        {
            foreach (PlaneModel plane in planes)
            {
                Console.WriteLine($" {plane.Id}. {plane.Name}");
            }

            if (!int.TryParse(Console.ReadLine(), out planeId))
            {
                Console.WriteLine("Invalid input. Please enter a valid plane ID:");
            }
            else if (!_planeLogic.doesPlaneExist(planeId))
            {
                Console.WriteLine("This plane does not exist, please choose another plane:");
            }
            else
            {
                validPlane = true;
            }
        }

        Console.WriteLine("\nFrom where?");
        string from = Console.ReadLine();

        Console.WriteLine("\nEnter a destination:");
        string destination = Console.ReadLine();
        while (destination == from)
        {
            Console.WriteLine("\nThe destination can not be the same as the departure, please try again:");
            destination = Console.ReadLine();
        }

        List<string> timeZones = new List<string>
        {
            "UTC-12:00",
            "UTC-11:00",
            "UTC-10:00",
            "UTC-09:00",
            "UTC-08:00",
            "UTC-07:00",
            "UTC-06:00",
            "UTC-05:00",
            "UTC-04:00",
            "UTC-03:00",
            "UTC-02:00",
            "UTC-01:00",
            "UTC±00:00",
            "UTC+01:00",
            "UTC+02:00",
            "UTC+03:00",
            "UTC+04:00",
            "UTC+05:00",
            "UTC+06:00",
            "UTC+07:00",
            "UTC+08:00",
            "UTC+09:00",
            "UTC+10:00",
            "UTC+11:00",
            "UTC+12:00"
        };

        Menu timezoneMenu = new Menu(timeZones, "Choose a timezone of the destination:");
        int chosenTimezone = timezoneMenu.Run();

        string timezone = _flightLogic.GetTimeZoneID(timeZones[chosenTimezone]);

        Console.WriteLine("\nEnter the flight duration in minutes:");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration))
        {
            Console.WriteLine("Invalid input. Please enter a valid flight duration:");
        }

        Console.WriteLine("\nDeparture date? (format: dd-MM-yyyy HH:mm):");
        string startDate = Console.ReadLine();

        bool validDate = false;
        while (!validDate)
        {
            if (!_flightLogic.IsValidDateFormat(startDate))
            {
                Console.WriteLine("Invalid date. Please enter a date with the correct format (dd-MM-yyyy HH:mm):");
                startDate = Console.ReadLine();
            }
            else if (!_flightLogic.IsValidDate(startDate))
            {
                Console.WriteLine("Invalid date. please enter a date that is not in the past:");
                startDate = Console.ReadLine();
            }
            else if (!_planeLogic.IsPlaneAvailable(planeId, startDate, duration))
            {
                Console.WriteLine("This date is not available for the plane you chose, please enter a different date:");
                startDate = Console.ReadLine();
            }
            else
            {
                validDate = true;
            }
        }

        Console.WriteLine("\nHow many days do you want to offer this flight?");
        int amountOfFlights = 1;
        bool validAmountFlights = false;
        while (!validAmountFlights)
        {
            if (!int.TryParse(Console.ReadLine(), out amountOfFlights))
            {
                Console.WriteLine("Invalid input. Please enter a valid amount of flights:");
            }
            else if (amountOfFlights <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a number higher then 0:");
            }
            else if (!_planeLogic.IsPlaneAvailable(planeId, startDate, duration, amountOfFlights))
            {
                Console.WriteLine("The plane you chose is not available for given amount of days");
            }
            else
            {
                validAmountFlights = true;
            }
        }

        Console.Clear();
        if (_flightLogic.CreateFlights(flightNumber, planeId, from, destination, timezone, duration, startDate, amountOfFlights))
        {
            Console.WriteLine("Flights succesfully created.");
        }
        else
        {
            Console.WriteLine("Could not create flights.");
        }

        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
        MainMenu.Start();
    }
}