using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservation_code")]
    public string ReservationCode { get; set; }

    [JsonPropertyName("reservation_date")]
    public string ReservationDate { get; set; }

    [JsonPropertyName("flight")]
    public FlightModel Flight { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("total_cost")]
    public double TotalCost { get; set; }

    [JsonPropertyName("passengers")]
    public List<PassengerModel> Passengers { get; set; }

    public ReservationModel(int id, string reservationCode, string reservationDate, FlightModel flight, int userId, double totalCost, List<PassengerModel> passengers)
    {
        Id = id;
        ReservationCode = reservationCode;
        ReservationDate = reservationDate;
        Flight = flight;
        UserId = userId;
        TotalCost = totalCost;
        Passengers = passengers;
    }
}

