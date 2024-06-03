using System.Text.Json;

public class FlightsAccess : IJsonHandler<FlightModel>
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/flights.json"));

    public List<FlightModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<FlightModel>>(json);
    }

    public void WriteAll(List<FlightModel> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }
}
