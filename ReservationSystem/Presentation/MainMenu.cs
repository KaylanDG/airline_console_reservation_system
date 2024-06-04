static class MainMenu
{

    public static void Start()
    {
        string prompt = @"
    ____        __  __                __                   ___    _      ___                
   / __ \____  / /_/ /____  _________/ /___ _____ ___     /   |  (_)____/ (_)___  ___  _____
  / /_/ / __ \/ __/ __/ _ \/ ___/ __  / __ `/ __ `__ \   / /| | / / ___/ / / __ \/ _ \/ ___/
 / _, _/ /_/ / /_/ /_/  __/ /  / /_/ / /_/ / / / / / /  / ___ |/ / /  / / / / / /  __(__  ) 
/_/ |_|\____/\__/\__/\___/_/   \__,_/\__,_/_/ /_/ /_/  /_/  |_/_/_/  /_/_/_/ /_/\\___/____/  

Choose an option from the menu.
Use the arrow keys to navigate the menu, press enter to select an option.
";

        List<string> options = new List<string>();

        if (AccountsLogic.CurrentAccount == null)
        {
            options.Add("Login");
            options.Add("Register");
        }
        else
        {
            options.Add("Logout");
            options.Add("Edit personal info");
            options.Add("My reservations");
            options.Add("Make reservation");
            if (AccountsLogic.CurrentAccount.Role == "admin")
            {
                options.Add("Create new flight(s)");
                options.Add("Delete flight(s)");
                options.Add("edit flight(s)");
                options.Add("Reservations overview");
                options.Add("Statistics");
                options.Add("Discount codes");
            }
        }

        options.Add("Flight overview");
        options.Add("Airport info");
        options.Add("Quit application");

        Menu menu = new Menu(options, prompt);
        int selectedOption = menu.Run();

        // Menu options for when user is not logged in
        if (AccountsLogic.CurrentAccount == null)
        {
            switch (selectedOption)
            {
                case 0:
                    UserLogin.Start();
                    break;
                case 1:
                    CreateAccount.Start();
                    break;
                case 2:
                    FlightOverview.Start();
                    break;
                case 3:
                    AirportInfo.Start();
                    break;
                case 4:
                    Environment.Exit(1);
                    break;
            }
        }
        // Menu options for when user is logged in and is admin
        else if (AccountsLogic.CurrentAccount.Role == "admin")
        {
            switch (selectedOption)
            {
                case 0:
                    AccountsLogic.Logout();
                    Start();
                    break;
                case 1:
                    PersonalInfoModify.Start();
                    break;
                case 2:
                    UserReservationOverview.Start();
                    break;
                case 3:
                    Reservation.Start();
                    break;
                case 4:
                    AddFlight.Start();
                    break;
                case 5:
                    RemoveFlight.Start();
                    break;
                case 6:
                    EditFlight.Start();
                    break;
                case 7:
                    AdminReservationOverview.Start();
                    break;
                case 8:
                    AdminStatistics.Start();
                    break;
                case 9:
                    Discount.Start();
                    break;
                case 10:
                    FlightOverview.Start();
                    break;
                case 11:
                    AirportInfo.Start();
                    break;
                case 12:
                    Environment.Exit(1);
                    break;
            }
        }
        // Menu options for when user is logged in
        else
        {
            switch (selectedOption)
            {
                case 0:
                    AccountsLogic.Logout();
                    Start();
                    break;
                case 1:
                    UserReservationOverview.Start();
                    break;
                case 2:
                    UserReservationOverview.Start();
                    break;
                case 3:
                    Reservation.Start();
                    break;
                case 4:
                    FlightOverview.Start();
                    break;
                case 5:
                    AirportInfo.Start();
                    break;
                case 6:
                    Environment.Exit(1);
                    break;
            }
        }

    }
}