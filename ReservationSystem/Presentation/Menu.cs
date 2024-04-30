public class Menu
{
    private int _selectedIndex;
    private List<string> _options;
    private string _prompt;
    private Action _method;

    public Menu(List<string> options, string prompt)
    {
        _selectedIndex = 0;
        _options = options;
        _prompt = prompt;
    }

    public Menu(List<string> options, string prompt, Action method)
    {
        _selectedIndex = 0;
        _options = options;
        _prompt = prompt;
        _method = method;
    }

    private void DisplayMenu()
    {
        _method?.Invoke();
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
            Console.Clear();
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
        }

        return _selectedIndex;
    }
}