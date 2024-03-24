using System.Collections.Generic;

public class ReservationLogic
{
    private List<ReservationModel> _reservations;
    private ReservationAccess _reservationAccess;

    public ReservationLogic()
    {
        _reservations = ReservationAccess.LoadAll();
        _reservationAccess = new ReservationAccess();
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

    // Method to retrieve all reservations
    public List<ReservationModel> GetAllReservations()
    {
        return _reservations;
    }
}
