public class ReservationLogic
{
    private List<ReservationModel> _reservations;

    public ReservationLogic()
    {
        _reservations = ReservationAccess.LoadAll();
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

        ReservationAccess.WriteAll(_reservations);
    }


    public ReservationModel CreateReservation(FlightModel flight, List<PassengerModel> passengers, double totalCost)
    {
        DateTime now = DateTime.Now;
        string reservationDate = now.ToString("dd-MM-yyyy HH:mm tt");


        ReservationModel newReservation = new ReservationModel(
            GenerateReservationId(),
            GenerateReservationCode(),
            reservationDate,
            flight,
            AccountsLogic.CurrentAccount.Id,
            totalCost,
            passengers
        );

        UpdateList(newReservation);
        return newReservation;
    }


    private int GenerateReservationId()
    {
        // Load existing reservations from the JSON file
        var reservations = ReservationAccess.LoadAll();

        // Find the highest existing ID
        int maxId = reservations.Max(r => r.Id);

        // Increment the highest existing ID by one to generate a new ID
        return maxId + 1;
    }

    private string GenerateReservationCode()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, 8)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
