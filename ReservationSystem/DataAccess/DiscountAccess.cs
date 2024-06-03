using System.Text.Json;

public class DiscountAccess : IJsonHandler<DiscountModel>
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/discount.json"));

    public List<DiscountModel> LoadAll()
    {
        if (!File.Exists(path) || new FileInfo(path).Length == 0)
        {
            return new List<DiscountModel>(); // Return an empty list if the file is empty or doesn't exist
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<DiscountModel>>(json);
    }

    public void WriteAll(List<DiscountModel> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }
}
