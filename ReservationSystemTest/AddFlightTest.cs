namespace ReservationSystemTest;

[TestClass]
public class AddFlightTest
{
    [TestMethod]
    public void TestFlightNumber()
    {
        FlightLogic logic = new FlightLogic();

        List<string> flightNumbers = new List<string>()
        {
            "KL1234", // valid
            "K1234", //invalid
            "1234KL", //invalid
            "K1234L", //invalid
            "KL 1234", //invalid
            "K!12#4", //invalid
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


        for (int i = 0; i < flightNumbers.Count; i++)
        {
            bool isValid = logic.IsValidFlightNumber(flightNumbers[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }

    [TestMethod]
    public void TestDateFormat()
    {
        FlightLogic logic = new FlightLogic();

        List<string> dates = new List<string>()
        {
            "10-05-2024 10:00", // valid
            "10-05-2024", //invalid
            "12-31-2024 10:00", //invalid
            "2024-10-05 10:00", //invalid
            "10:00", //invalid
            "2024-05-10 10:00", //invalid
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


        for (int i = 0; i < dates.Count; i++)
        {
            bool isValid = logic.IsValidDateFormat(dates[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }

    [TestMethod]
    public void TestDate()
    {
        FlightLogic logic = new FlightLogic();

        List<string> dates = new List<string>()
        {
            "01-12-2024 10:00", // valid
            "01-01-2024 10:00", //invalid
        };

        List<bool> outcomes = new List<bool>()
        {
            true,
            false,
        };


        for (int i = 0; i < dates.Count; i++)
        {
            bool isValid = logic.IsValidDate(dates[i]);
            Assert.AreEqual(isValid, outcomes[i]);
        }
    }

    [TestMethod]
    public void TestPlaneAvailable()
    {
        FlightLogic flightLogic = new FlightLogic();
        flightLogic.CreateFlights("test", 1, "test", "test", "GMT", 60, "16-06-2024 10:00", 1); // Arrival = 16-06-2024 11:00

        PlaneLogic planeLogic = new PlaneLogic();

        Assert.AreEqual(false, planeLogic.IsPlaneAvailable(1, "16-06-2024 09:30", 60, 1));
        Assert.AreEqual(false, planeLogic.IsPlaneAvailable(1, "16-06-2024 10:30", 60, 1));
        Assert.AreEqual(true, planeLogic.IsPlaneAvailable(1, "16-06-2024 12:00", 60, 1));
    }
}