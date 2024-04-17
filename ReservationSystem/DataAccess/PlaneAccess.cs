using System.Text.Json;

public static class PlaneAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/planes.json"));

    public static List<PlaneModel> LoadAllPlanes()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<PlaneModel>>(json);
    }

    public static void WriteAll(List<PlaneModel> planes)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(planes, options);
        File.WriteAllText(path, json);
    }
}