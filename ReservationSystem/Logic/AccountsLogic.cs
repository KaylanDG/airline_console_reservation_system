using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public bool ValidEmail(string email)
    {
        if (email.Contains("@") && email.Contains(".") && email != null)
        {
            return true;
        }
        else
        {
            // Console.WriteLine("Incorrect E-Mail format.");
            return false;
        }
    }

    public bool ValidPassword(string password)
    {
        if (password.Length > 7)
        {
            return true;
        }
        else
        {
            // Console.WriteLine("Password needs to be at least 8 characters.");
            return false;
        }
    }

    public bool ValidPhone(string phone)
    {
        if (phone[0] == "0" && phone.Length == 10 && phone.All(char.IsDigit))
        {
            return true;
        }
        else
        {
            // Console.WriteLine("Phone number needs to be at least 10 characters.");
            // Console.WriteLine("Start with a \"0\".");
            // Console.WriteLine("And needs to be all numbers.");
            return false;

        }
    }

    public bool ValidInput(string input)
    {
        if (input != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public AccountModel CreateAccount(string email, string password, string FullName, string Phone, string DateOfBirth, bool Invalid, string role = "user")
    {
        int id_var = 0;
        foreach (AccountModel s in _accounts)
        {
            if (s.EmailAddress == email)
            {
                // Console.WriteLine("Account with this E-mail Adress already exists.")
                return null;
            }
            if (s.Id > id_var)
            {
                id_var = s.Id;
            }
        }


        id_var++;
        AccountModel acc = new AccountModel(id_var, email, password, FullName, Phone, DateOfBirth, Invalid, role);
        UpdateList(acc);
        CurrentAccount = acc;
        return acc;

    }
}




