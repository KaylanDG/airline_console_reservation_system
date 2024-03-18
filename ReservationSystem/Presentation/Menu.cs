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
        Console.WriteLine("L | Login");
        Console.WriteLine("F | Flight overview");

        string input = Console.ReadLine().ToLower();
        if (input == "l")
        {
            UserLogin.Start();
        }
        else if (input == "f")
        {
            FlightOverview.Start();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}