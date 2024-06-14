using System;

public static class Discount
{
    private static DiscountLogic _discountLogic = new DiscountLogic();
    private static DiscountAccess _discountsAccess = new DiscountAccess();

    private static List<DiscountModel> _discounts;

    public static void Start()
    {
        Menu discountMenu = new Menu(new List<string> { "Create new code", "Go back" }, "Choose an option", DiscountOverview);
        int option = discountMenu.Run();

        if (option == 0) CreateDiscount();
        else MainMenu.Start();
    }

    public static void DiscountOverview()
    {

        _discounts = _discountsAccess.LoadAll();
        Console.WriteLine("| {0,-20} | {1,-20} | {2,-20} | {3,-20} |", "ID", "Code", "Start date", "End date");
        Console.WriteLine(new string('-', 93));
        foreach (DiscountModel d in _discounts)
        {
            Console.WriteLine("| {0,-20} | {1,-20} | {2,-20} | {3,-20} |", d.Id, d.DiscountCode, d.DiscountDateFrom, d.DiscountDateTill);
        }
    }

    public static void CreateDiscount()
    {
        Console.Clear();
        Console.WriteLine("Enter Discount Code:");
        string discountCode = Console.ReadLine();
        while (_discountLogic.DoesCodeExist(discountCode))
        {
            Console.WriteLine("The entered code already exist, please try again");
            discountCode = Console.ReadLine();
        }

        Console.WriteLine("Enter Discount Percentage (1-100):");
        int discountPercentage = 0;
        while (!int.TryParse(Console.ReadLine(), out discountPercentage) || discountPercentage <= 0 || discountPercentage > 100)
        {
            Console.WriteLine("Invalid discount percentage. Please enter a number between 1 and 100. Try again:");

        }

        Console.WriteLine("Enter Discount Start Date (dd-MM-yyyy HH:mm):");
        string discountDateFrom = Console.ReadLine();
        while (!_discountLogic.IsValidDateFormat(discountDateFrom) || !_discountLogic.IsValidDate(discountDateFrom))
        {
            Console.WriteLine("Invalid start date or date is in the past. Try again:");
            discountDateFrom = Console.ReadLine();
        }

        Console.WriteLine("Enter Discount End Date (dd-MM-yyyy HH:mm):");
        string discountDateTill = Console.ReadLine();
        while (!_discountLogic.IsValidDateFormat(discountDateTill) || !_discountLogic.IsValidDate(discountDateTill))
        {
            Console.WriteLine("Invalid end date or date is in the past. Try again:");
            discountDateTill = Console.ReadLine();
        }

        try
        {
            _discountLogic.CreateDiscount(discountCode, discountPercentage, discountDateFrom, discountDateTill);
            Console.WriteLine("Discount code created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating discount: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to return..");
        Console.ReadKey(true);

        Console.Clear();
        MainMenu.Start();
    }
}
