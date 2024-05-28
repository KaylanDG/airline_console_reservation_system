public static class AdminStatistics
{
    private static DateTime _today = DateTime.Now;
    private static List<String> _viewOptions = new List<string> { "All time", "Monthly", "Weekly", "Daily" };
    private static int _selectedOption;
    private static DateTime _searchDate;

    public static void Start()
    {
        _selectedOption = 0;
        bool stop = false;

        while (!stop)
        {
            Console.Clear();
            Statistics();
            ViewOptions();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.DownArrow)
            {
                _selectedOption++;
                if (_selectedOption == _viewOptions.Count) _selectedOption = 0;
            }
            else if (pressedKey == ConsoleKey.UpArrow)
            {
                _selectedOption--;
                if (_selectedOption == -1) _selectedOption = _viewOptions.Count - 1;
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
        string stats = $"║ Profit: €0 ║ Reservations: 100 ║ New Users: 0 ║";
        string border = new string('═', stats.Length - 2);
        Console.WriteLine("╔" + border + "╗");
        Console.WriteLine(stats);
        Console.WriteLine("╚" + border + "╝");
    }
}