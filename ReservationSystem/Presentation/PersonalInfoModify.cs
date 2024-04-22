public static class PersonalInfoModify
{
    public static void Start()
    {
        Console.Clear();
        //Option on what to modify.
        Console.WriteLine("E | Change E-Mail");
        Console.WriteLine("P | Change Password");
        Console.WriteLine("T | Change Telephone Number");
        Console.WriteLine("D | Change Date of birth");
        Console.WriteLine("F | Change Full name");

        string ans = Console.ReadLine().ToLower();

        // Check if the string is an valid option.
        // Otherwise loop

        while (ans != "e" && ans != "p" && ans != "t" && ans != "d" && ans != "f")
        {
            Console.WriteLine("That's not a valid option on the menu");
            Console.WriteLine("Choose an valid option.");
        }

        if (ans == "e")
        {
            PersonalInfoModify.EmailModify();
        }

        if (ans == "p")
        {
            PersonalInfoModify.PasswordModify();
        }
    }

    public static void EmailModify()
    {
        AccountsLogic x = new AccountsLogic();

        Console.WriteLine($"Old Email: {AccountsLogic.CurrentAccount}");
        Console.WriteLine("What do you want your new Email to be?");
        string NewEmail = Console.ReadLine();

        while (!x.ValidEmail(NewEmail) || AccountsLogic.CurrentAccount.EmailAddress == NewEmail)
        {
            // New E-mail can't be the same as the current E-mail.
            // ^^ Wouldn't make sense.
            Console.WriteLine("That's an invalid Email or it is the same as the current Email, try again: ");
            NewEmail = Console.ReadLine();
        }
        // Password enteren for confirm
        // voor damian
        // mezelf reminder
        AccountsLogic.CurrentAccount.EmailAddress = NewEmail;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Email");
    }

    public static void PasswordModify()
    {
        AccountsLogic x = new AccountsLogic();

        // Has to enter current password to change it.
        Console.WriteLine($"First enter your current password: ");
        string oldpass = Console.ReadLine();
        while (oldpass != x.Password)
        {
            Console.WriteLine("That's not your current password, try again.")
            Console.WriteLine("Current Password: ");
            oldpass = Console.ReadLine();
        }

        Console.WriteLine("What do you want your new Password to be?");
        string NewPassword = Console.ReadLine();

        while (!x.ValidPassword(NewPassword) || AccountsLogic.CurrentAccount.Password == NewPassword)
        {
            // New Password can't be the same as the current Password.
            // ^^ Wouldn't make sense.
            Console.WriteLine("That's an invalid Password");
            Console.WriteLine("Your password needs to be at least 8 characters.");
            Console.WriteLine("It also can't be your old password.");
            NewwPassword = Console.ReadLine();
        }

        AccountsLogic.CurrentAccount.Password = NewPassword;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Password");
    }
}