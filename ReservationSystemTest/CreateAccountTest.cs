namespace ReservationSystemTest;

[TestClass]
public class CreateAccountTest
{

    [TestMethod]
    public void TestValidName()
    {
        AccountsLogic logic = new AccountsLogic();

        List<string> names = new List<string>()
        {
            "Damian van Dams", // valid
            "Kaylan1 de Groot", //invalid because containing a number
            "Jay# Kumar", //invalid contains a special character 
            "#!&*^$", //invalid contains a special character
            "12377", //invalid contains numbers 
            "hjasdghagdjhajkasdhjkasd hjkahdjkshdkjasdgsjdhgashds", //invalid too long of a name.
        };

        List<bool> outcomes = new List<bool>()
        {
            true,
            false,
            false,
            false,
            false,
            false,
        };


        for (int i = 0; i < names.Count; i++)
        {
            bool isValid = logic.ValidName(names[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }

    }

    [TestMethod]
    public void TestValidEmail()
    {

        AccountsLogic logic = new AccountsLogic();

        List<string> emails = new List<string>()
        {
            "test@gmail.com", // valid
            "test @gmail.com", //invalid contains a space
            "testgmail.com", //invalid doesn't contain a @
            "test@gmailcom", //invalid doesn't contain a .
            "test gmail com", //invalid contains spaces and doesnt contain an @ and a .
        };

        List<bool> outcomes = new List<bool>()
        {
            true,
            false,
            false,
            false,
            false,
        };

        for (int i = 0; i < emails.Count; i++)
        {
            bool isValid = logic.ValidEmail(emails[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }

    [TestMethod]
    public void TestValidPhone()
    {
        AccountsLogic logic = new AccountsLogic();

        List<string> numbers = new List<string>()
        {
            "0612345678", // valid
            "1612345678", //invalid doenst start with a 0.
            "0123", //invalid doesnt contain 10 character
            "+061234567", //invalid doesn't start with a 0.
            "06123456789", //invalid has 11 character instead of 10
            "0 612345678", //invalid contains a space
            "061234567A", //invalid contains a letter
        };

        List<bool> outcomes = new List<bool>()
        {
            true,
            false,
            false,
            false,
            false,
            false,
            false,
        };

        for (int i = 0; i < numbers.Count; i++)
        {
            bool isValid = logic.ValidPhone(numbers[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }

    [TestMethod]
    public void TestValidPassword()
    {
        AccountsLogic logic = new AccountsLogic();

        List<string> passwords = new List<string>()
        {
            "testtest123", // valid 
            "abc", //invalid doesn't contain at least 8 characters
            "abcdfeg", //invalid doesnt contain 8 characters
            "abcdfegh", //valid
            "abc dfegh", //invalid contains a space
        };

        List<bool> outcomes = new List<bool>()
        {
            true,
            false,
            false,
            true,
            false,
        };

        for (int i = 0; i < passwords.Count; i++)
        {
            bool isValid = logic.ValidPassword(passwords[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }
}