using System.Globalization;

public class DiscountLogic
{
    private List<DiscountModel> _discounts;

    public DiscountLogic()
    {
        _discounts = DiscountAccess.LoadAllDiscounts();
    }

    public bool CreateDisount(string discountcode, int discountpercentage, string discountdatefrom, string discountdatetill)
    {
        DateTime now = DateTime.Now;
        string reservationDate = now.ToString("dd-MM-yyyy HH:mm");
        DiscountModel discount = new DiscountModel(
        GenerateDiscountId(),
        discountcode,
        discountpercentage,
        discountdatefrom,
        discountdatetill
        );
        UpdateList(discount);
        return true;
    }

    public bool IsValidDateFormat(string input)
    {
        // Check if date has correct format
        string format = "dd-MM-yyyy HH:mm";
        DateTime parsedDate;
        //return true or false
        return DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out parsedDate);
    }


    public bool IsValidDate(string input)
    {
        //check if date is not in the past
        DateTime now = DateTime.Now;
        DateTime date = DateTime.ParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

        if (date < now) return false;
        return true;
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
        var discounts = DiscountAccess.LoadAllDiscounts();

        int maxId = discounts.Max(r => r.Id);

        return maxId + 1;
    }

    public bool RemoveDiscount(string DiscountCode)
    {
        // load in all reservations
        List<DiscountModel> _discounts = DiscountAccess.LoadAllDiscounts();
        bool removed = false;

        // foreach reservation check if reservation code is equal to given reservation code
        // if so remove reservation from list
        foreach (DiscountModel x in _discounts)
        {
            if (x.DiscountCode == DiscountCode)
            {
                _discounts.Remove(x);
                removed = true;
                break;
            }
        }
        // after removing reservation update json
        DiscountAccess.WriteAll(_discounts);
        return removed;
    }

}