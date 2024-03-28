static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.Clear();

        Console.WriteLine("\n██       ██████   ██████  ██ ███    ██ ");
        Console.WriteLine("██      ██    ██ ██       ██ ████   ██");
        Console.WriteLine("██      ██    ██ ██   ███ ██ ██ ██  ██");
        Console.WriteLine("██      ██    ██ ██    ██ ██ ██  ██ ██");
        Console.WriteLine("███████  ██████   ██████  ██ ██   ████\n");

        Console.WriteLine("\nPlease enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("\nPlease enter your password");
        string password = Console.ReadLine();

        // Check if account exists
        AccountModel acc = accountsLogic.CheckLogin(email, password);

        // Show message if user is logged in successfully or not
        // And return to menu
        if (acc != null)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWelcome back " + acc.FullName);
            Console.ResetColor();

        }
        else
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nNo account found with that email and password\n");
            Console.ResetColor();
        }

        Menu.Start();
    }
}