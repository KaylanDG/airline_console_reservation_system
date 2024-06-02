namespace ReservationSystemTest;

[TestClass]
public class ReservationTests
{

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
    public void TestExtraLuggagePrice()
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
    public void TestMaximumLuggage()
    {
        FlightLogic flightLogic = new FlightLogic();
        ReservationLogic reservationLogic = new ReservationLogic();

        // Flight with plane Boeing 737
        flightLogic.CreateFlights("test", 1, "test", "test", "GMT", 60, "16-06-2024 10:00", 1);
        FlightModel newFlight1 = FlightsAccess.LoadAllFlights()[^1];
        Assert.AreEqual(132, flightLogic.MaxLuggageAmount(newFlight1.Id));

        ReservationModel reservation1 = new ReservationModel();
        PassengerModel passenger1 = new PassengerModel(1);
        passenger1.AdditionalServices.Add(new ServiceModel("Extra Luggage", 1, 25));
        reservation1.Passengers.Add(passenger1);
        reservation1.FlightId = newFlight1.Id;
        reservationLogic.CompleteReservation(reservation1);

        Assert.AreEqual(131, flightLogic.MaxLuggageAmount(newFlight1.Id));


        // Flight with plane Boeing 787
        flightLogic.CreateFlights("test", 2, "test", "test", "GMT", 60, "16-06-2024 10:00", 1);
        FlightModel newFlight2 = FlightsAccess.LoadAllFlights()[^1];
        Assert.AreEqual(400, flightLogic.MaxLuggageAmount(newFlight2.Id));

        ReservationModel reservation2 = new ReservationModel();
        PassengerModel passenger2 = new PassengerModel(1);
        passenger2.AdditionalServices.Add(new ServiceModel("Extra Luggage", 1, 25));
        reservation2.Passengers.Add(passenger2);
        reservation2.FlightId = newFlight2.Id;
        reservationLogic.CompleteReservation(reservation2);

        Assert.AreEqual(399, flightLogic.MaxLuggageAmount(newFlight2.Id));

        // Flight with plane Airbus 330
        flightLogic.CreateFlights("test", 3, "test", "test", "GMT", 60, "16-06-2024 10:00", 1);
        FlightModel newFlight3 = FlightsAccess.LoadAllFlights()[^1];
        Assert.AreEqual(400, flightLogic.MaxLuggageAmount(newFlight3.Id));

        ReservationModel reservation3 = new ReservationModel();
        PassengerModel passenger3 = new PassengerModel(1);
        passenger3.AdditionalServices.Add(new ServiceModel("Extra Luggage", 1, 25));
        reservation3.Passengers.Add(passenger3);
        reservation3.FlightId = newFlight3.Id;
        reservationLogic.CompleteReservation(reservation3);

        Assert.AreEqual(399, flightLogic.MaxLuggageAmount(newFlight3.Id));

    }

    [TestMethod]
    public void TestFlightExist()
    {
        FlightLogic logic = new FlightLogic();
        List<FlightModel> flights = logic.GetAvailableFlights();
        Assert.AreEqual(false, logic.DoesFlightExist(1928321312));
        Assert.AreEqual(true, logic.DoesFlightExist(flights[0].Id));
    }

    [TestMethod]
    public void TestSeatExist()
    {
        PlaneLogic planeLogic = new PlaneLogic();

        Assert.AreEqual(true, planeLogic.DoesSeatExist("B-01", 1));
        Assert.AreEqual(false, planeLogic.DoesSeatExist("01-B", 1));

    }

    [TestMethod]
    public void TestSeatReserved()
    {
        ReservationLogic reservationLogic = new ReservationLogic();
        FlightLogic flightLogic = new FlightLogic();

        flightLogic.CreateFlights("test", 1, "test", "test", "GMT", 60, "16-06-2024 10:00", 1);
        FlightModel newFlight = FlightsAccess.LoadAllFlights()[^1];

        ReservationModel reservation = new ReservationModel();
        PassengerModel passenger = new PassengerModel(1);
        passenger.SeatNumber = "A-01";
        reservation.Passengers.Add(passenger);
        reservation.FlightId = newFlight.Id;

        reservationLogic.CompleteReservation(reservation);
        Assert.AreEqual(false, flightLogic.IsSeatReserved("B-01", flightLogic.GetFlightSeats(newFlight)));
        Assert.AreEqual(true, flightLogic.IsSeatReserved("A-01", flightLogic.GetFlightSeats(newFlight)));
    }


    [TestMethod]
    public void TestSeatSelected()
    {
        FlightLogic flightLogic = new FlightLogic();
        PlaneLogic planelogic = new PlaneLogic();

        List<SeatModel> seats = planelogic.GetPlaneSeats(1);
        seats = flightLogic.UpdateFlightSeats("C-01", seats);

        Assert.AreEqual(false, flightLogic.IsSeatSelected("B-01", seats));
        Assert.AreEqual(true, flightLogic.IsSeatSelected("C-01", seats));

    }
}