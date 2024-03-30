using System.Collections.Generic;

public class ReservationLogic
{
    private List<Reservation> _reservations;
    private ReservationAccess _reservationAccess;
    private FlightLogic _flightLogic;

    public ReservationLogic()
    {
        _reservations = ReservationAccess.LoadAll();
        _reservationAccess = new ReservationAccess();
        _flightLogic = new FlightLogic();
    }

    public Reservation CreateReservation(int flightID, string passengerName)
    {
        DateTime now = DateTime.Now;
        string reservationDate = now.ToString("dd-MM-yyyy HH:mm tt");
        int userID = AccountsLogic.CurrentAccount.Id;

        // Has to be changed
        List<Passenger> passengers = new List<Passenger>
        {
            new Passenger(1, passengerName, "")
        };

        Flight flight = _flightLogic.GetById(flightID);

        // Still has to be implemented:
        // Seats
        // Total cost calculation

        Reservation newReservation = new Reservation(
            GenerateReservationId(),
            GenerateRandomReservationCode(),
            reservationDate,
            flight,
            userID,
            0.0,
            passengers
        );

        UpdateList(newReservation);
        return newReservation;
    }

    public void UpdateList(Reservation reservation)
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

    private int GenerateReservationId()
    {
        // Create an instance of ReservationAccess
        var reservationAccess = new ReservationAccess();

        // Load existing reservations from the JSON file
        var reservations = ReservationAccess.LoadAll();

        // Find the highest existing ID
        int maxId = reservations.Max(r => r.Id);

        // Increment the highest existing ID by one to generate a new ID
        return maxId + 1;
    }

    private string GenerateRandomReservationCode()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, 8)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
