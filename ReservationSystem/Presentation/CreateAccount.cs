public class CreateAccount
{
    public static void Start()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        bool stop = false;

        while (!stop)
        {
            Console.Clear();
            Console.WriteLine("\n    ____             _      __           ");
            Console.WriteLine("   / __ \\___  ____ _(_)____/ /____  _____");
            Console.WriteLine("  / /_/ / _ \\/ __ `/ / ___/ __/ _ \\/ ___/");
            Console.WriteLine(" / _, _/  __/ /_/ / (__  ) /_/  __/ /    ");
            Console.WriteLine("/_/ |_|\\___/\\__, /_/____/\\__/\\___/_/     ");
            Console.WriteLine("           /____/                        \n");


            Console.WriteLine("\nPlease enter your Full Name");
            string fullName = Console.ReadLine();

            Console.WriteLine("\nPlease enter your email address");
            string email = Console.ReadLine();

            if (!accountsLogic.ValidEmail(email))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nIncorrect E-Mail format.\n");
                Console.ResetColor();
                break;
            }

            Console.WriteLine("\nPlease enter your password");
            string password = Console.ReadLine();

            if (!accountsLogic.ValidPassword(password))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nPassword needs to be at least 8 characters.\n");
                Console.ResetColor();
                break;
            }

            Console.WriteLine("\nPlease enter your phone number");
            string phone = Console.ReadLine();

            if (!accountsLogic.ValidPhone(phone))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nPhone number needs to be at least 10 characters.");
                Console.WriteLine("Start with a \"0\".");
                Console.WriteLine("And needs to be all numbers.\n");
                Console.ResetColor();
                break;
            }

            Console.WriteLine("\nPlease enter your date of birth");
            string dateOfBirth = Console.ReadLine();
            while (!accountsLogic.ValidDateOfBirth(dateOfBirth))
            {
                Console.WriteLine("\nThat's not a valid date of birth.");
                Console.WriteLine("Please enter your date of birth");
                dateOfBirth = Console.ReadLine();
            }


            bool invalid = false;
            Console.WriteLine("\nAre you disabled (yes/no)?");
            string userInput = Console.ReadLine().ToLower();


            if (userInput == "yes")
            {
                invalid = true;
            }

            var AlreadyExist = accountsLogic.CreateAccount(email, password, fullName, phone, dateOfBirth, invalid);
            if (AlreadyExist != null)
            {
                Console.WriteLine("Succesfully Signed up.");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nAccount with this E-mail Adress already exists.\n");
                Console.ResetColor();
            }
            break;
        }


        Menu.Start();
    }
}