namespace ReservationSystemTest;

[TestClass]
public class CreateAccountTest
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"../../../../ReservationSystem/DataSources/accounts.json"));

    [TestInitialize]
    public void SetPath()
    {
        AccountsAccess.path = path;
    }


    [TestMethod]
    public void TestValidName()
    {
        AccountsLogic logic = new AccountsLogic();

        List<string> names = new List<string>()
        {
            "Damian van Dams", // valid
            "Kaylan1 de Groot", //invalid
            "Jay# Kumar", //invalid
            "#!&*^$", //invalid
            "12377", //invalid
            "hjasdghagdjhajkasdhjkasd hjkahdjkshdkjasdgsjdhgashds", //invalid
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
            "test @gmail.com", //invalid
            "testgmail.com", //invalid
            "test@gmailcom", //invalid
            "test gmail com", //invalid
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
            "1612345678", //invalid
            "0123", //invalid
            "+061234567", //invalid
            "06123456789", //invalid
            "0 612345678", //invalid
            "061234567A", //invalid
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
            "abc", //invalid
            "abcdfeg", //invalid
            "abcdfegh", //valid
            "abc dfegh", //invalid
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