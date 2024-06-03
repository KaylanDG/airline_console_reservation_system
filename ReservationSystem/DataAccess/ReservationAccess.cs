using System.Text.Json;

public class ReservationAccess : IJsonHandler<ReservationModel>
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));

    public List<ReservationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ReservationModel>>(json);
    }

    public void WriteAll(List<ReservationModel> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }
}
