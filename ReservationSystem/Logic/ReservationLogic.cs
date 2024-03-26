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

    public void CreateReservation(string flightNumber, string passengerName)
    {
        DateTime now = DateTime.Now;
        string reservationDate = now.ToString("dd-MM-yyyy HH:mm tt");
        int userID = AccountsLogic.CurrentAccount.Id;

        List<Passenger> passengers = new List<Passenger>
        {
            new Passenger(1, passengerName, "")
        };



        // Still has to be implemented:
        // Seats
        // Reservation Code
        // Total cost calculation

        Reservation newReservation = new Reservation(
            GenerateReservationId(),
            "",
            reservationDate,
            flightNumber,
            userID,
            0.0,
            passengers
        );

        UpdateList(newReservation);
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

    public bool DoesFlightExist(string flightNumber)
    {
        // Check if given flight number is in the list of available flights
        foreach (var flight in _flightLogic.GetAvailableFlights())
        {
            if (flight.FlightNumber == flightNumber)
            {
                return true;
            }
        }
        return false;
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
}
