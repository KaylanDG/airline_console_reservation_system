using System.Text.Json.Serialization;

public class Plane
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("airline")]
    public string Airline { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("passengers")]
    public int Passengers { get; set; }
}