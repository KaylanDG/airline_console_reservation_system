using System.Text.Json;

public class PlaneAccess : IJsonHandler<PlaneModel>
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/planes.json"));

    public List<PlaneModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<PlaneModel>>(json);
    }

    public void WriteAll(List<PlaneModel> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }
}