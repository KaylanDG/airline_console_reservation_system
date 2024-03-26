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
                Console.WriteLine("C | Create account");
            }
            else
            {
                Console.WriteLine("R | Make an reservation");
            }
            Console.WriteLine("F | Flight overview");
            Console.WriteLine("A | Airport info");
            Console.WriteLine("Q | Quit program");

            input = Console.ReadLine().ToLower();

            if (input == "l" && AccountsLogic.CurrentAccount == null)
            {
                UserLogin.Start();
            }
            else if (input == "c" && AccountsLogic.CurrentAccount == null)
            {
                CreateAccount.Start();
            }
            else if (input == "r")
            {
                ReservationMenu.Start();
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
                Console.WriteLine("Invalid input");
                Start();
            }
        }

    }
}