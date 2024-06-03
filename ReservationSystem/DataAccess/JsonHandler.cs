using System.Text.Json;

public abstract class JsonHandler<T>
{
    protected string path;

    public List<T> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json);
        }
        catch (Exception)
        {
            return new List<T>();
        }
    }

    public void WriteAll(List<T> data)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while writing the data to file.", ex);
        }

    }
}