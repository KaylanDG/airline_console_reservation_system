using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservation_code")]
    public string ReservationCode { get; set; }

    [JsonPropertyName("reservation_date")]
    public string ReservationDate { get; set; }

    [JsonPropertyName("flight_number")]
    public string FlightNumber { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("total_cost")]
    public decimal TotalCost { get; set; }

    [JsonPropertyName("passengers")]
    public List<PassengerModel> Passengers { get; set; }

}

