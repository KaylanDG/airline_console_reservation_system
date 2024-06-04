using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    private List<AccountModel> _accounts;
    private AccountsAccess _accountsAccess = new AccountsAccess();

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = _accountsAccess.LoadAll();
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
        _accountsAccess.WriteAll(_accounts);

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

    public AccountModel CreateAccount(string email, string password, string FullName, string Phone, string DateOfBirth, bool Invalid, string role = "user")
    {
        int id_var = 0;
        foreach (AccountModel s in _accounts)
        {
            if (s.EmailAddress == email)
            {
                // 
                return null;
            }
            if (s.Id > id_var)
            {
                id_var = s.Id;
            }
        }


        id_var++;
        string dateCreated = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        AccountModel acc = new AccountModel(id_var, email, password, FullName, Phone, DateOfBirth, Invalid, role, dateCreated);
        UpdateList(acc);
        CurrentAccount = acc;
        return acc;

    }

    public static void Logout()
    {
        CurrentAccount = null;
    }

    public bool ValidEmail(string email)
    {
        if (email.Contains("@") && email.Contains(".") && email != null && !email.Contains(" "))
        {
            return true;
        }
        else
        {
            // 
            return false;
        }
    }

    public bool ValidPassword(string password)
    {
        if (password.Length > 7 && !password.Contains(" "))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ValidPhone(string phone)
    {
        if (phone.Length == 10 && phone.All(char.IsDigit) && phone[0] == '0')
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    public bool ValidName(string name)
    {
        Regex nameRegex = new Regex(@"^[A-Za-z ]+$");

        if (name.Length <= 50 && nameRegex.IsMatch(name) && name != null)
        {
            return true;
        }
        return false;
    }

    public bool ValidDateOfBirth(string date)
    {
        DateTime result;
        bool conversionSuccessful = DateTime.TryParse(date, out result);
        return conversionSuccessful;
    }
}




