public class CreateAccount
{
    public static void Start()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        bool stop = false;

        while (!stop)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the sign up page\n");
            Console.WriteLine("Please enter your Full Name");
            string fullName = Console.ReadLine();


            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine();

            if (!accountsLogic.ValidEmail(email))
            {
                Console.WriteLine("Incorrect E-Mail format.");
                break;
            }

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            if (!accountsLogic.ValidPassword(password))
            {
                Console.WriteLine("Password needs to be at least 8 characters.");
                break;
            }

            Console.WriteLine("Please enter your phone number");
            string phone = Console.ReadLine();

            if (!accountsLogic.ValidPhone(phone))
            {
                Console.WriteLine("Phone number needs to be at least 10 characters.");
                Console.WriteLine("Start with a \"0\".");
                Console.WriteLine("And needs to be all numbers.");
                break;
            }

            Console.WriteLine("Please enter your date of birth");
            string dateOfBirth = Console.ReadLine();


            bool invalid = false;
            Console.WriteLine("Are you invalid (yes/no)?");
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
                Console.WriteLine("Account with this E-mail Adress already exists.");
            }

        }


        Menu.Start();
    }
}