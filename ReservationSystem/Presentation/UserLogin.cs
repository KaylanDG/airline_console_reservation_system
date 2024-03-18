static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the login page\n");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();

        // Check if account exists
        AccountModel acc = accountsLogic.CheckLogin(email, password);

        // Show message if user is logged in successfully or not
        // And return to menu
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine("Welcome back " + acc.FullName);

        }
        else
        {
            Console.Clear();
            Console.WriteLine("No account found with that email and password");
        }

        Menu.Start();
    }
}