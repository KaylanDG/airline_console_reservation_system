using System.Runtime.CompilerServices;

static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("\nChoose one of the menu options:");
        Console.WriteLine(new string('-', 20));
        if (AccountsLogic.CurrentAccount == null)
        {
            Console.WriteLine("L | Login");
            Console.WriteLine("C | Create account");
        }
        Console.WriteLine("F | Flight overview");

        string input = Console.ReadLine().ToLower();

        if (input == "l" && AccountsLogic.CurrentAccount == null)
        {
            UserLogin.Start();
        }
        else if (input == "c" && AccountsLogic.CurrentAccount == null)
        {
            CreateAccount.Start();
            // Add create-account menu start method
        }
        else if (input == "f")
        {
            FlightOverview.Start();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}