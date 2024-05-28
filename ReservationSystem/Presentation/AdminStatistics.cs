using System.Globalization;
public static class AdminStatistics
{
    private static DateTime _date;
    private static List<String> _viewOptions = new List<string> { "All time", "Monthly", "Weekly", "Daily" };
    private static int _selectedOption;
    private static string _searchDate;
    private static StatisticLogic _statisticLogic = new StatisticLogic();
    public static void Start()
    {
        _date = DateTime.Now;
        _selectedOption = 0;
        bool stop = false;

        while (!stop)
        {
            Console.Clear();
            Console.WriteLine("Press the up/down arrow to navigate the menu");
            Console.WriteLine("Press the left/right arrow to change date");
            Console.WriteLine("Press Backspace to return the main menu");

            Statistics();
            ViewOptions();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.DownArrow)
            {
                _date = DateTime.Now;
                _selectedOption++;
                if (_selectedOption == _viewOptions.Count) _selectedOption = 0;
            }
            else if (pressedKey == ConsoleKey.UpArrow)
            {
                _date = DateTime.Now;
                _selectedOption--;
                if (_selectedOption == -1) _selectedOption = _viewOptions.Count - 1;
            }
            else if (pressedKey == ConsoleKey.LeftArrow)
            {
                switch (_selectedOption)
                {
                    case 0:
                        break;
                    case 1:
                        _date = _date.AddMonths(-1);
                        break;
                    case 2:
                        _date = _date.AddDays(-7);
                        break;
                    case 3:
                        _date = _date.AddDays(-1);
                        break;
                }
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                switch (_selectedOption)
                {
                    case 0:
                        break;
                    case 1:
                        _date = _date.AddMonths(1);
                        break;
                    case 2:
                        _date = _date.AddDays(7);
                        break;
                    case 3:
                        _date = _date.AddDays(1);
                        break;
                }
            }
            else if (pressedKey == ConsoleKey.Backspace)
            {
                stop = true;
                MainMenu.Start();
            }
        }
    }

    public static void ViewOptions()
    {
        for (int i = 0; i < _viewOptions.Count; i++)
        {
            string prefix = " ";
            if (i == _selectedOption)
            {
                prefix = ">";
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{prefix} {_viewOptions[i]}");
            Console.ResetColor();
        }
    }

    public static void Statistics()
    {
        (double, int, int) data = _statisticLogic.GetStatistics();

        switch (_selectedOption)
        {
            case 0:
                _searchDate = "All time";
                break;
            case 1:
                data = _statisticLogic.GetStatisticsMonthly(_date);
                _searchDate = _date.ToString("Y", CultureInfo.CreateSpecificCulture("en-US"));
                break;
            case 2:
                (DateTime, DateTime) week = _statisticLogic.GetWeekBoundaries(_date);
                data = _statisticLogic.GetStatisticsWeekly(week.Item1, week.Item2);
                _searchDate = week.Item1.ToString("D", CultureInfo.CreateSpecificCulture("en-US")) + " / " + week.Item2.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
                break;
            case 3:
                data = _statisticLogic.GetStatisticsDaily(_date);
                _searchDate = _date.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
                break;
        }

        string stats = $"║ Profit: €{data.Item1} ║ Reservations: {data.Item2} ║ New Users: {data.Item3} ║";
        string border = new string('═', stats.Length - 2);

        Console.WriteLine($"\n[{_searchDate}]\n");
        Console.WriteLine("╔" + border + "╗");
        Console.WriteLine(stats);
        Console.WriteLine("╚" + border + "╝");
    }
}