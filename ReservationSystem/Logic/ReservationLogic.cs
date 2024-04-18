public class ReservationLogic
{
    private List<ReservationModel> _reservations;
    public List<ReservationModel> SavedReservations;

    public ReservationLogic()
    {
        _reservations = ReservationAccess.LoadAll();
        SavedReservations = new List<ReservationModel>();
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

    public void CompleteReservation()
    {
        foreach (ReservationModel reservation in SavedReservations)
        {
            reservation.Id = GenerateReservationId();
            UpdateList(reservation);
        }
    }

    public void SaveReservation(ReservationModel reservation)
    {
        DateTime now = DateTime.Now;
        string reservationDate = now.ToString("dd-MM-yyyy HH:mm");

        reservation.ReservationCode = GenerateReservationCode();
        reservation.ReservationDate = reservationDate;

        SavedReservations.Add(reservation);
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

    public double ExtraLuggagePrice(int howmany)
    {
        double TotalPrice = 0.0;
        for (int i = 1; i <= howmany; i++)
        {
            TotalPrice += i * 25;
        }
        return TotalPrice;
    }

    public double GetTotalCost()
    {
        double totalCost = 0;
        foreach (ReservationModel reservation in SavedReservations)
        {
            totalCost += reservation.TotalCost;
        }
        return totalCost;
    }

    public List<ReservationModel> ListOfReservationMethod()
    {
        int AccountID = AccountsLogic.CurrentAccount.Id;
        List<ReservationModel> _reservations = ReservationAccess.LoadAll();
        List<ReservationModel> ReturnReservation = new List<ReservationModel>();
        foreach (ReservationModel x in _reservations)
        {
            if (AccountID == x.UserId)
            {
                ReturnReservation.Add(x);
            }
        }
        return ReturnReservation;
    }

    // Load alle reservaties
    // loop je door die reservaties
    // x.ResvCode == para && x.UserID == AccountLogic.CurrentAccount.Id
    // Lijst mer reservaties.remove(x) && break loop
    // ReservationAccess.WriteAll(---);

    public static bool RemoveReservation(string ReservationCode)
    {
        List<ReservationModel> _reservations = ReservationAccess.LoadAll();
        bool removed = false;
        foreach (ReservationModel x in _reservations)
        {
            if (x.ReservationCode == ReservationCode && x.UserId == AccountsLogic.CurrentAccount.Id)
            {
                _reservations.Remove(x);
                removed = true;
                break;
            }
        }
        ReservationAccess.WriteAll(_reservations);
        return removed;
    }
}
