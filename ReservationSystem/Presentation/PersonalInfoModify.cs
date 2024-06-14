public static class PersonalInfoModify
{
    public static void Start()
    {
        List<string> options = new List<string>()
        {
            "Change E-Mail",
            "Change Password",
            "Change Telephone Number",
            "Change your Full name",
            "Change Date of birth",
            "Change your disability",
            "Go back to main menu",
        };

        string prompt = "Choose what you want to edit:";
        Menu menu = new Menu(options, prompt);
        int selectedOption = menu.Run();
        Console.Clear();


        if (selectedOption == 0)
        {
            PersonalInfoModify.EmailModify();
        }

        else if (selectedOption == 1)
        {
            PersonalInfoModify.PasswordModify();
        }
        else if (selectedOption == 2)
        {
            PersonalInfoModify.PhoneModify();
        }

        else if (selectedOption == 3)
        {
            PersonalInfoModify.FullNameModify();
        }
        else if (selectedOption == 4)
        {
            PersonalInfoModify.DateOfBirthModify();
        }
        else if (selectedOption == 5)
        {
            PersonalInfoModify.ChangeDisabilityModify();
        }

        MainMenu.Start();
    }

    public static void EmailModify()
    {
        AccountsLogic x = new AccountsLogic();

        Console.WriteLine($"Old Email: {AccountsLogic.CurrentAccount.EmailAddress}");
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
        Console.WriteLine("Enter your password to confirm your Email change.");
        string pas = Console.ReadLine();

        while (pas != AccountsLogic.CurrentAccount.Password)
        {
            Console.WriteLine("That's not your password");
            Console.WriteLine("Try again: ");
            pas = Console.ReadLine();
        }

        AccountsLogic.CurrentAccount.EmailAddress = NewEmail;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Email");
        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
    }

    public static void PasswordModify()
    {
        AccountsLogic x = new AccountsLogic();

        // Has to enter current password to change it.
        Console.WriteLine($"First enter your current password: ");
        string oldpass = Console.ReadLine();
        while (oldpass != AccountsLogic.CurrentAccount.Password)
        {
            Console.WriteLine("That's not your current password, try again.");
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
            NewPassword = Console.ReadLine();
        }

        AccountsLogic.CurrentAccount.Password = NewPassword;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Password");
        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
    }

    public static void FullNameModify()
    {
        AccountsLogic x = new AccountsLogic();

        Console.WriteLine($"Full name: {AccountsLogic.CurrentAccount.FullName}");
        Console.WriteLine("What do you want your new Full name to be?");
        string NewFullname = Console.ReadLine();

        while (!x.ValidName(NewFullname) || AccountsLogic.CurrentAccount.FullName == NewFullname)
        {
            // New Full name can't be the same as the current Fullname.
            // ^^ Wouldn't make sense.
            Console.WriteLine("That's an invalid Full name or it is the same as the current Full name, try again: ");
            NewFullname = Console.ReadLine();
        }
        // Password enteren for confirm
        // voor damian
        // mezelf reminder
        Console.WriteLine("Enter your password to confirm your Full name change.");
        string pas = Console.ReadLine();

        while (pas != AccountsLogic.CurrentAccount.Password)
        {
            Console.WriteLine("That's not your password");
            Console.WriteLine("Try again: ");
            pas = Console.ReadLine();
        }

        AccountsLogic.CurrentAccount.FullName = NewFullname;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Full name");
        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
    }


    public static void PhoneModify()
    {
        AccountsLogic x = new AccountsLogic();

        Console.WriteLine($"Phone number: {AccountsLogic.CurrentAccount.Phone}");
        Console.WriteLine("What do you want your new Phone number to be?");
        string NewPhone = Console.ReadLine();

        while (!x.ValidPhone(NewPhone) || AccountsLogic.CurrentAccount.Phone == NewPhone)
        {
            // New Phone can't be the same as the current Phone.
            // ^^ Wouldn't make sense.
            Console.WriteLine("That's an invalid Phone or it is the same as the current Phone, try again: ");
            NewPhone = Console.ReadLine();
        }
        // Password enteren for confirm
        // voor damian
        // mezelf reminder
        Console.WriteLine("Enter your password to confirm your Phone number change.");
        string pas = Console.ReadLine();

        while (pas != AccountsLogic.CurrentAccount.Password)
        {
            Console.WriteLine("That's not your password");
            Console.WriteLine("Try again: ");
            pas = Console.ReadLine();
        }

        AccountsLogic.CurrentAccount.Phone = NewPhone;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Phone number");
        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
    }

    public static void DateOfBirthModify()
    {
        AccountsLogic x = new AccountsLogic();

        Console.WriteLine($"Date of birth: {AccountsLogic.CurrentAccount.DateOfBirth}");
        Console.WriteLine("What do you want your new Date of Birth to be?");
        string newDateOfBirth = Console.ReadLine();

        while (!x.ValidDateOfBirth(newDateOfBirth) || AccountsLogic.CurrentAccount.DateOfBirth == newDateOfBirth)
        {
            // New Date of Birth can't be the same as the current Date of Birth.
            // ^^ Wouldn't make sense.
            Console.WriteLine("That's an invalid Date of Birth or it is the same as the current DateOfBirth, try again: ");
            newDateOfBirth = Console.ReadLine();
        }
        // Password enteren for confirm
        // voor damian
        // mezelf reminder
        Console.WriteLine("Enter your password to confirm your Date of Birth change.");
        string pas = Console.ReadLine();

        while (pas != AccountsLogic.CurrentAccount.Password)
        {
            Console.WriteLine("That's not your password");
            Console.WriteLine("Try again: ");
            pas = Console.ReadLine();
        }

        AccountsLogic.CurrentAccount.DateOfBirth = newDateOfBirth;
        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Date of Birth");
        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
    }

    public static void ChangeDisabilityModify()
    {
        AccountsLogic x = new AccountsLogic();

        if (AccountsLogic.CurrentAccount.Disabled == true)
        {
            AccountsLogic.CurrentAccount.Disabled = false;
            Console.WriteLine($"Disability set to {AccountsLogic.CurrentAccount.Disabled}");
        }
        else
        {
            AccountsLogic.CurrentAccount.Disabled = true;
            Console.WriteLine($"Disability set to {AccountsLogic.CurrentAccount.Disabled}");
        }
        Console.WriteLine("Enter your password to confirm your Disability change.");
        string pas = Console.ReadLine();

        while (pas != AccountsLogic.CurrentAccount.Password)
        {
            Console.WriteLine("That's not your password");
            Console.WriteLine("Try again: ");
            pas = Console.ReadLine();
        }


        x.UpdateList(AccountsLogic.CurrentAccount);
        Console.WriteLine("Succesfully changed your Disability");
        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);
    }



}