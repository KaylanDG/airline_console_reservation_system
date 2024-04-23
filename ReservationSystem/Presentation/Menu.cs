public class Menu
{
    private int _selectedIndex;
    private List<string> _options;
    private string _prompt;

    public Menu(List<string> options, string prompt)
    {
        _selectedIndex = 0;
        _options = options;
        _prompt = prompt;
    }

    private void DisplayMenu()
    {
        Console.WriteLine(_prompt);

        for (int i = 0; i < _options.Count; i++)
        {

            string prefix = " ";
            if (i == _selectedIndex)
            {
                prefix = ">";
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{prefix} {_options[i]}");
            Console.ResetColor();
        }
    }

    public int Run()
    {

        ConsoleKey pressedKey = default;

        while (pressedKey != ConsoleKey.Enter)
        {
            DisplayMenu();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            pressedKey = keyInfo.Key;

            if (pressedKey == ConsoleKey.UpArrow)
            {
                _selectedIndex--;
                if (_selectedIndex == -1) _selectedIndex = _options.Count - 1;
            }
            else if (pressedKey == ConsoleKey.DownArrow)
            {
                _selectedIndex++;
                if (_selectedIndex == _options.Count) _selectedIndex = 0;
            }
            Console.Clear();
        }

        return _selectedIndex;
    }
}