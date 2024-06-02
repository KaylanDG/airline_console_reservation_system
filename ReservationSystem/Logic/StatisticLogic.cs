using System.Globalization;

public class StatisticLogic
{
    private List<ReservationModel> _reservations;
    private List<AccountModel> _accounts;
    private FlightLogic _flightLogic;


    public StatisticLogic()
    {
        _reservations = ReservationAccess.LoadAll();
        _accounts = AccountsAccess.LoadAll();
        _flightLogic = new FlightLogic();
    }

    public (double, int, int, string) GetStatistics(List<AccountModel> accounts, List<ReservationModel> reservations)
    {
        double totalProfit = reservations.Sum(x => x.TotalCost);
        int totalReservations = reservations.Count;
        int totalNewUsers = accounts.Count(x => x.Role == "user");
        string popularDestination = "";

        if (reservations.Count > 0)
        {
            Dictionary<string, int> destinations = new Dictionary<string, int>();

            foreach (ReservationModel resv in reservations)
            {
                FlightModel flight = _flightLogic.GetById(resv.FlightId);
                if (destinations.ContainsKey(flight.Destination)) destinations[flight.Destination] += 1;
                else destinations[flight.Destination] = 1;
            }

            popularDestination = destinations.MaxBy(x => x.Value).Key;
        }

        return new(totalProfit, totalReservations, totalNewUsers, popularDestination);
    }

    public (double, int, int, string) GetStatistics()
    {
        return GetStatistics(_accounts, _reservations);
    }

    public (double, int, int, string) GetStatisticsMonthly(DateTime date)
    {
        List<ReservationModel> filteredResv = _reservations
        .Where(x =>
        {
            DateTime resvDate = DateTime.ParseExact(x.ReservationDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return resvDate.Month == date.Month && resvDate.Year == date.Year;
        })
        .ToList();

        List<AccountModel> filteredAcc = _accounts
        .Where(x =>
        {
            DateTime accDate = DateTime.ParseExact(x.DateCreated, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return accDate.Month == date.Month && accDate.Year == date.Year;
        })
        .ToList();

        return GetStatistics(filteredAcc, filteredResv);
    }

    public (double, int, int, string) GetStatisticsWeekly(DateTime startDate, DateTime endDate)
    {
        List<ReservationModel> filteredResv = _reservations
        .Where(x =>
        {
            DateTime resvDate = DateTime.ParseExact(x.ReservationDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return resvDate >= startDate && resvDate <= endDate;
        })
        .ToList();

        List<AccountModel> filteredAcc = _accounts
        .Where(x =>
        {
            DateTime accDate = DateTime.ParseExact(x.DateCreated, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return accDate >= startDate && accDate <= endDate;
        })
        .ToList();

        return GetStatistics(filteredAcc, filteredResv);
    }

    public (double, int, int, string) GetStatisticsDaily(DateTime date)
    {
        List<ReservationModel> filteredResv = _reservations
        .Where(x =>
        {
            DateTime resvDate = DateTime.ParseExact(x.ReservationDate, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return resvDate.Month == date.Month && resvDate.Year == date.Year && resvDate.Day == date.Day;
        })
        .ToList();

        List<AccountModel> filteredAcc = _accounts
        .Where(x =>
        {
            DateTime accDate = DateTime.ParseExact(x.DateCreated, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return accDate.Month == date.Month && accDate.Year == date.Year && accDate.Day == date.Day;
        })
        .ToList();

        return GetStatistics(filteredAcc, filteredResv);
    }



    public (DateTime FirstDayOfWeek, DateTime LastDayOfWeek) GetWeekBoundaries(DateTime date)
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        int diff = (7 + (date.DayOfWeek - firstDayOfWeek)) % 7;

        DateTime firstDay = date.AddDays(-1 * diff).Date;
        DateTime lastDay = firstDay.AddDays(6).Date;

        return (firstDay, lastDay);
    }
}