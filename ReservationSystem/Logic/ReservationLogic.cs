class ReservationLogic
{
    private FlightLogic _flightLogic;

    public ReservationLogic()
    {
        _flightLogic = new FlightLogic();
    }

    public bool DoesFlightExist(string flightNumber)
    {
        foreach (var flight in _flightLogic.GetAvailableFlights())
        {
            if (flight.FlightNumber == flightNumber)
            {
                return true;
            }
        }
        return false;
    }
}