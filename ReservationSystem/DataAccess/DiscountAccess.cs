using System.Text.Json;

public static class DiscountAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/discount.json"));

    public static List<DiscountModel> LoadAllDiscounts()
    {
        if (!File.Exists(path) || new FileInfo(path).Length == 0)
        {
            return new List<DiscountModel>(); // Return an empty list if the file is empty or doesn't exist
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<DiscountModel>>(json);
    }

    public static void WriteAll(List<DiscountModel> discounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(discounts, options);
        File.WriteAllText(path, json);
    }
}
