using System.Text.Json;

public class FlightsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/flights.json"));

    public static List<Flight> LoadAllFlights()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Flight>>(json);
    }

    public void WriteAll(List<Flight> flights)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(flights, options);
        File.WriteAllText(path, json);
    }
}
