using System.Globalization;

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

    // This method completes the reservation by adding a reservation date
    // a reservation code and a reservation ID
    // After the new reservation gets saved the reservation code is returned so it
    // can be shown in the presentation layer
    public string CompleteReservation(ReservationModel reservation)
    {
        DateTime now = DateTime.Now;
        string reservationDate = now.ToString("dd-MM-yyyy HH:mm");

        reservation.Id = GenerateReservationId();
        reservation.ReservationCode = GenerateReservationCode();
        reservation.ReservationDate = reservationDate;
        UpdateList(reservation);
        return reservation.ReservationCode;
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
        // Create a string of all possible characters and numbers
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        // Generate a new reservation code by randomly selecting a character from the string 8 times
        return new string(Enumerable.Repeat(chars, 8)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public double ExtraLuggagePrice(int howmany)
    {
        double TotalPrice = 0.0;
        // foreach piece of extra luggage update the total price
        for (int i = 1; i <= howmany; i++)
        {
            TotalPrice += i * 25;
        }
        // return total price of extra luggage
        return TotalPrice;
    }

    public List<ReservationModel> ListOfReservationMethod()
    {
        // Get id of current account
        int AccountID = AccountsLogic.CurrentAccount.Id;

        // load in all reservations
        List<ReservationModel> _reservations = ReservationAccess.LoadAll();

        // create new list to store reservation of current account
        List<ReservationModel> ReturnReservation = new List<ReservationModel>();

        // foreach reservation check if user id is equal to id of current user
        foreach (ReservationModel x in _reservations)
        {
            if (AccountID == x.UserId)
            {
                ReturnReservation.Add(x);
            }
        }
        return ReturnReservation;
    }

    public bool RemoveReservation(string ReservationCode)
    {
        // load in all reservations
        List<ReservationModel> _reservations = ReservationAccess.LoadAll();
        bool removed = false;

        // foreach reservation check if reservation code is equal to given reservation code
        // if so remove reservation from list
        foreach (ReservationModel x in _reservations)
        {
            if (x.ReservationCode == ReservationCode && x.UserId == AccountsLogic.CurrentAccount.Id)
            {
                _reservations.Remove(x);
                removed = true;
                break;
            }
        }
        // after removing reservation update json
        ReservationAccess.WriteAll(_reservations);
        return removed;
    }

    public List<ReservationModel> GetReservationsForPage(List<ReservationModel> reservations, int page, int pageSize)
    {
        //Get the starting index
        int startIndex = (page - 1) * pageSize;
        //Get the size of the sublist
        int count = Math.Min(pageSize, reservations.Count - startIndex);
        // return sublist
        return reservations.GetRange(startIndex, count);
    }

    public List<ReservationModel> SearchReservations<T>(string filter, T value)
    {
        List<ReservationModel> reservations = new List<ReservationModel>();

        foreach (ReservationModel reservation in ReservationAccess.LoadAll())
        {
            if (filter == "userId")
            {
                if (reservation.UserId.Equals(value)) reservations.Add(reservation);
            }
            else if (filter == "reservationCode")
            {
                if (reservation.ReservationCode.Equals(value)) reservations.Add(reservation);
            }
            else if (filter == "reservationDate")
            {
                string date = reservation.ReservationDate.Split(" ")[0];
                if (date.Equals(value)) reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public bool IsValidDateFormat(string input)
    {
        // Check if date has correct format
        string format = "dd-MM-yyyy";
        DateTime parsedDate;
        //return true or false
        return DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out parsedDate);
    }
}
