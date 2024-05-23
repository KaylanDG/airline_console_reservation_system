using System.Text.Json.Serialization;


public class DiscountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("discountcode")]
    public string DiscountCode { get; set; }

    [JsonPropertyName("discountpercentage")]
    public string DiscountPercentage { get; set; }

    [JsonPropertyName("discountdatefrom")]
    public string DiscountDateFrom { get; set; }

    [JsonPropertyName("discountdatetill")]
    public string DiscountDateTill { get; set; }


    public DiscountModel(int id, string discountCode, string discountPercentage, string discountDateFrom, string discountDateTill)
    {
        Id = id;
        DiscountCode = discountCode;
        DiscountPercentage = discountPercentage;
        DiscountDateFrom = discountDateFrom;
        DiscountDateTill = discountDateTill;
    }

}




