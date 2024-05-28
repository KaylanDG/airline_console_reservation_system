using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class DiscountLogic
{
    private List<DiscountModel> _discounts;

    public DiscountLogic()
    {
        _discounts = DiscountAccess.LoadAllDiscounts();
    }

    public bool CreateDiscount(string discountCode, int discountPercentage, string discountDateFrom, string discountDateTill)
    {
        if (!IsValidDateFormat(discountDateFrom) || !IsValidDateFormat(discountDateTill))
        {
            throw new ArgumentException("Invalid date format. Use dd-MM-yyyy HH:mm.");
        }

        if (!IsValidDate(discountDateFrom) || !IsValidDate(discountDateTill))
        {
            throw new ArgumentException("Discount dates must be in the future.");
        }

        if (discountPercentage <= 0 || discountPercentage > 100)
        {
            throw new ArgumentException("Discount percentage must be between 1 and 100.");
        }

        DiscountModel discount = new DiscountModel(
            GenerateDiscountId(),
            discountCode,
            discountPercentage,
            discountDateFrom,
            discountDateTill
        );

        UpdateList(discount);
        return true;
    }

    public bool IsValidDateFormat(string input)
    {
        string format = "dd-MM-yyyy HH:mm";
        return DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out _);
    }

    public bool IsValidDate(string input)
    {
        DateTime date = DateTime.ParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        return date > DateTime.Now;
    }

    public void UpdateList(DiscountModel discount)
    {
        int index = _discounts.FindIndex(r => r.Id == discount.Id);

        if (index != -1)
        {
            _discounts[index] = discount;
        }
        else
        {
            _discounts.Add(discount);
        }

        DiscountAccess.WriteAll(_discounts);
    }

    private int GenerateDiscountId()
    {
        if (_discounts.Count == 0) return 1;
        int maxId = _discounts.Max(r => r.Id);
        return maxId + 1;
    }

    public bool RemoveDiscount(string discountCode)
    {
        int index = _discounts.FindIndex(d => d.DiscountCode == discountCode);
        if (index != -1)
        {
            _discounts.RemoveAt(index);
            DiscountAccess.WriteAll(_discounts);
            return true;
        }
        return false;
    }

    public DiscountModel GetValidDiscount(string discountCode)
    {
        var discount = _discounts.FirstOrDefault(d => d.DiscountCode == discountCode);
        if (discount == null)
        {
            throw new ArgumentException("Discount code not found.");
        }

        DateTime dateFrom = DateTime.ParseExact(discount.DiscountDateFrom, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        DateTime dateTill = DateTime.ParseExact(discount.DiscountDateTill, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        DateTime now = DateTime.Now;

        if (now < dateFrom || now > dateTill)
        {
            throw new InvalidOperationException("Discount code is not valid at this time.");
        }

        return discount;
    }

    public bool ApplyDiscount(string discountCode)
    {
        try
        {
            var discount = GetValidDiscount(discountCode);

            Console.WriteLine($"Discount code {discountCode} applied with {discount.DiscountPercentage}% off.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying discount: {ex.Message}");
            return false;
        }
    }
}
