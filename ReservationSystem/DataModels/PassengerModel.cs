using System.Text.Json.Serialization;

public class PassengerModel
{
    [JsonPropertyName("passenger_id")]
    public int PassengerId { get; set; }
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
    [JsonPropertyName("seat_number")]
    public string SeatNumber { get; set; }
}