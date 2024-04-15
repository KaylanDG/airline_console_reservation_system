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

    public ReservationModel(int id, string reservationCode, string reservationDate, int flightId, int userId, double totalCost, List<PassengerModel> passengers)
    {
        Id = id;
        ReservationCode = reservationCode;
        ReservationDate = reservationDate;
        FlightId = flightId;
        UserId = userId;
        TotalCost = totalCost;
        Passengers = passengers;
    }
}

