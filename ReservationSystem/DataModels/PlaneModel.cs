using System.Text.Json.Serialization;

public class PlaneModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("airline")]
    public string Airline { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("economy_seats")]
    public int EconomySeats { get; set; }

    [JsonPropertyName("first_class_seats")]
    public int FirstClassSeats { get; set; }


}