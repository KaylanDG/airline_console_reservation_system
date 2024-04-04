using System.Text.Json;

public class PlaneAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/planes.json"));

    public static List<Plane> LoadAllPlanes()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Plane>>(json);
    }

    public void WriteAll(List<Plane> planes)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(planes, options);
        File.WriteAllText(path, json);
    }
}