using System;

public class Discount
{
    private DiscountLogic _discountLogic;

    public Discount()
    {
        _discountLogic = new DiscountLogic();
    }

    public void CreateDiscount()
    {
        Console.WriteLine("Enter Discount Code:");
        string discountCode = Console.ReadLine();

        Console.WriteLine("Enter Discount Percentage (1-100):");
        if (!int.TryParse(Console.ReadLine(), out int discountPercentage) || discountPercentage <= 0 || discountPercentage > 100)
        {
            Console.WriteLine("Invalid discount percentage. Please enter a number between 1 and 100.");
            return;
        }

        Console.WriteLine("Enter Discount Start Date (dd-MM-yyyy HH:mm):");
        string discountDateFrom = Console.ReadLine();
        if (!_discountLogic.IsValidDateFormat(discountDateFrom) || !_discountLogic.IsValidDate(discountDateFrom))
        {
            Console.WriteLine("Invalid start date or date is in the past.");
            return;
        }

        Console.WriteLine("Enter Discount End Date (dd-MM-yyyy HH:mm):");
        string discountDateTill = Console.ReadLine();
        if (!_discountLogic.IsValidDateFormat(discountDateTill) || !_discountLogic.IsValidDate(discountDateTill))
        {
            Console.WriteLine("Invalid end date or date is in the past.");
            return;
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
    }
}
