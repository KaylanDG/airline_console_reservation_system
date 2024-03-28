using System.Text.Json.Serialization;

public class Reservation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservation_code")]
    public string ReservationCode { get; set; }

    [JsonPropertyName("reservation_date")]
    public string ReservationDate { get; set; }

    [JsonPropertyName("flight")]
    public Flight Flight { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("total_cost")]
    public double TotalCost { get; set; }

    [JsonPropertyName("passengers")]
    public List<Passenger> Passengers { get; set; }

    public Reservation(int id, string reservationCode, string reservationDate, Flight flight, int userId, double totalCost, List<Passenger> passengers)
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

