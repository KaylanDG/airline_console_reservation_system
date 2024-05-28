using System.Globalization;

public class StatisticLogic
{
    private List<ReservationModel> _reservations;
    private List<AccountModel> _accounts;


    public StatisticLogic()
    {
        _reservations = ReservationAccess.LoadAll();
        _accounts = AccountsAccess.LoadAll();
    }

    public (double, int, int) GetStatistics()
    {
        double totalProfit = _reservations.Sum(x => x.TotalCost);
        int totalReservations = _reservations.Count;
        int totalNewUsers = _accounts.Count(x => x.Role == "user");

        return new(totalProfit, totalReservations, totalNewUsers);
    }

    public (double, int, int) GetStatisticsMonthly(DateTime date)
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

        double totalProfit = filteredResv.Sum(x => x.TotalCost);
        int totalReservations = filteredResv.Count;
        int totalNewUsers = filteredAcc.Count(x => x.Role == "user");

        return new(totalProfit, totalReservations, totalNewUsers);
    }

    public (double, int, int) GetStatisticsWeekly(DateTime startDate, DateTime endDate)
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

        double totalProfit = filteredResv.Sum(x => x.TotalCost);
        int totalReservations = filteredResv.Count;
        int totalNewUsers = filteredAcc.Count(x => x.Role == "user");

        return new(totalProfit, totalReservations, totalNewUsers);
    }

    public (double, int, int) GetStatisticsDaily(DateTime date)
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

        double totalProfit = filteredResv.Sum(x => x.TotalCost);
        int totalReservations = filteredResv.Count;
        int totalNewUsers = filteredAcc.Count(x => x.Role == "user");

        return new(totalProfit, totalReservations, totalNewUsers);
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