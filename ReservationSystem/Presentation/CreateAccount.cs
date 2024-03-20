public class CreateAccount
{
    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the login page\n");
        Console.WriteLine("Please enter your Full Name");
        string fullName = Console.ReadLine();
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        Console.WriteLine("Please enter your phone number");
        string phone = Console.ReadLine();
        Console.WriteLine("Please enter your date of birth");
        string dateOfBirth = Console.ReadLine();

        bool invalid = false;
        Console.WriteLine("Are you invalid (yes/no)?");
        string userInput = Console.ReadLine().ToLower();
        if (userInput == "yes")
        {
            invalid = true;
        }

        AccountsLogic accountsLogic = new AccountsLogic();

        //


        Menu.Start();
    }
}