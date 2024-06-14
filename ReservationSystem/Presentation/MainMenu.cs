static class MainMenu
{
    private static bool _isAdmin = false;
    private static string _prompt = @"
    ____        __  __                __                   ___    _      ___                
   / __ \____  / /_/ /____  _________/ /___ _____ ___     /   |  (_)____/ (_)___  ___  _____
  / /_/ / __ \/ __/ __/ _ \/ ___/ __  / __ `/ __ `__ \   / /| | / / ___/ / / __ \/ _ \/ ___/
 / _, _/ /_/ / /_/ /_/  __/ /  / /_/ / /_/ / / / / / /  / ___ |/ / /  / / / / / /  __(__  ) 
/_/ |_|\____/\__/\__/\___/_/   \__,_/\__,_/_/ /_/ /_/  /_/  |_/_/_/  /_/_/_/ /_/\\___/____/  

Choose an option from the menu.
Use the arrow keys to navigate the menu, press enter to select an option.
";

    public static void Start()
    {

        List<string> options = new List<string>();

        if (AccountsLogic.CurrentAccount == null)
        {
            options.Add("Login");
            options.Add("Register");
            options.Add("Flight overview");
            options.Add("Airport info");
            options.Add("Quit application");
        }
        else
        {
            _isAdmin = AccountsLogic.CurrentAccount.Role == "admin" ? true : false;
            options.Add("Logout");
            options.Add("Edit personal info");
            options.Add("My reservations");
            options.Add("Make reservation");
            options.Add("Flight overview");
            options.Add("Airport info");
            if (_isAdmin)
            {
                options.Add("Admin menu");
            }
            options.Add("Quit application");
        }


        Menu menu = new Menu(options, _prompt);
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
        // Menu options for when user is logged in
        else
        {
            if (selectedOption == 0)
            {
                AccountsLogic.Logout();
                Start();
            }
            else if (selectedOption == 1) PersonalInfoModify.Start();
            else if (selectedOption == 2) UserReservationOverview.Start();
            else if (selectedOption == 3) Reservation.Start();
            else if (selectedOption == 4) FlightOverview.Start();
            else if (selectedOption == 5) AirportInfo.Start();
            else if (selectedOption == 6 && _isAdmin) AdminMenu();
            else if (selectedOption == 6 && !_isAdmin) Environment.Exit(1);
            else if (selectedOption == 7 && _isAdmin) Environment.Exit(1);
        }

    }

    private static void AdminMenu()
    {
        List<string> adminOptions = new List<string>()
        {
            "Create new flight(s)",
            "Delete flight(s)",
            "edit flight(s",
            "Reservations overview",
            "Statistics",
            "Discount codes",
            "Go back"
        };

        Menu adminMenu = new Menu(adminOptions, _prompt);
        int adminOption = adminMenu.Run();

        if (adminOption == 0) AddFlight.Start();
        else if (adminOption == 1) RemoveFlight.Start();
        else if (adminOption == 2) EditFlight.Start();
        else if (adminOption == 3) AdminReservationOverview.Start();
        else if (adminOption == 4) AdminStatistics.Start();
        else if (adminOption == 5) Discount.Start();
        else if (adminOption == 6) Start();
    }
}