static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        string input = "";
        while (input != "q")
        {
            Console.WriteLine("\nChoose one of the menu options:");
            Console.WriteLine(new string('-', 20));
            if (AccountsLogic.CurrentAccount == null)
            {
                Console.WriteLine("L | Login");
                Console.WriteLine("R | Register");
            }
            else
            {
                Console.WriteLine("R | Make an reservation");
            }
            Console.WriteLine("F | Flight overview");
            Console.WriteLine("A | Airport info");
            if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Role == "admin")
            {
                Console.WriteLine("A | Add an flight to json");
            }

            if (AccountsLogic.CurrentAccount != null)
            {
                Console.WriteLine("L | Logout");
            }
            Console.WriteLine("Q | Quit program");

            input = Console.ReadLine().ToLower();

            if (input == "l" && AccountsLogic.CurrentAccount == null)
            {
                UserLogin.Start();
            }
            if (input == "l" && AccountsLogic.CurrentAccount != null)
            {
                AccountsLogic.Logout();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("successfully logged out!");
                Console.ResetColor();
                Start();
            }
            else if (input == "r" && AccountsLogic.CurrentAccount == null)
            {
                CreateAccount.Start();
            }
            else if (input == "r" && AccountsLogic.CurrentAccount != null)
            {
                FlightOverview.ShowOverview();
                Reservation.Start();
            }
            else if (input == "f")
            {
                FlightOverview.Start();
            }
            else if (input == "q")
            {
                Environment.Exit(1);
            }
            else if (input == "a")
            {
                AirportInfo.Start();
            }
            else
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nInvalid input\n");
                Console.ResetColor();
                Start();
            }
        }

    }
}