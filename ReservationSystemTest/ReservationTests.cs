namespace ReservationSystemTest;

[TestClass]
public class ReservationTests
{

    [TestInitialize]
    public void SetPath()
    {
        AccountsAccess.path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"../../../../ReservationSystem/DataSources/accounts.json"));
        FlightsAccess.path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"../../../../ReservationSystem/DataSources/flights.json"));
        ReservationAccess.path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"../../../../ReservationSystem/DataSources/reservations.json"));
        PlaneAccess.path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"../../../../ReservationSystem/DataSources/planes.json"));
    }

    [TestMethod]
    public void TestSeatPrice()
    {
        List<PlaneModel> planes = PlaneAccess.LoadAllPlanes();
        PlaneLogic logic = new PlaneLogic();
        foreach (PlaneModel plane in planes)
        {
            foreach (SeatModel seat in logic.GetPlaneSeats(plane.Id))
            {
                if (seat.SeatType == "First Class")
                {
                    Assert.AreEqual(5, seat.PricePerMinute);
                }
                else
                {
                    Assert.AreEqual(4, seat.PricePerMinute);
                }
            }
        }
    }

    [TestMethod]
    public void TestExtraLuggage()
    {
        ReservationLogic logic = new ReservationLogic();
        double expectedPrice = 0;

        for (int i = 0; i <= 15; i++)
        {
            expectedPrice += 25 * i;
            Assert.AreEqual(expectedPrice, logic.ExtraLuggagePrice(i));
        }
    }

    [TestMethod]
    public void TestFlightExist()
    {
        FlightLogic logic = new FlightLogic();
        List<FlightModel> flights = logic.GetAvailableFlights();
        Assert.AreEqual(false, logic.DoesFlightExist(1928321312));
        Assert.AreEqual(true, logic.DoesFlightExist(flights[0].Id));
    }
}