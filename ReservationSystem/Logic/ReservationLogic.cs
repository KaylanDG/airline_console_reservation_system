using System.Collections.Generic;

public class ReservationLogic
{
    private List<ReservationModel> _reservations;
    private ReservationAccess _reservationAccess;

    private FlightLogic _flightLogic;

    public ReservationLogic()
    {
        _reservationAccess = new ReservationAccess();
        _flightLogic = new FlightLogic();
    }


    public void UpdateList(ReservationModel reservation)
    {
        int index = _reservations.FindIndex(r => r.Id == reservation.Id);

        if (index != -1)
        {
            _reservations[index] = reservation;
        }
        else
        {
            _reservations.Add(reservation);
        }

        _reservationAccess.WriteAll(_reservations);
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
