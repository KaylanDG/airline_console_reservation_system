using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservation_code")]
    public string ReservationCode { get; set; }

    [JsonPropertyName("reservation_date")]
    public string ReservationDate { get; set; }

    [JsonPropertyName("flight_id")]
    public int FlightId { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("total_cost")]
    public double TotalCost { get; set; }

    [JsonPropertyName("passengers")]
    public List<PassengerModel> Passengers { get; set; }

    public ReservationModel()
    {
        Passengers = new List<PassengerModel>();
    }
}

