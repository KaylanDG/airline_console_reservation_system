namespace ReservationSystemTest;

[TestClass]
public class CreateAccountTest
{
    [TestMethod]
    public void TestValidName()
    {
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
            bool isValid = AccountsLogic.ValidName(names[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }

    }

    [TestMethod]
    public void TestValidEmail()
    {
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
            bool isValid = AccountsLogic.ValidEmail(emails[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }

    public void TestValidPhone()
    {
        List<string> numbers = new List<string>()
        {
            "0612345678", // valid
            "1612345678", //invalid
            "0123", //invalid
            "+061234567", //invalid
            "06123456789", //invalid
            "0 612345678", //invalid
        };
    }
}