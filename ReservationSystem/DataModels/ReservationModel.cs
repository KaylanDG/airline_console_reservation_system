using System.Text.Json.Serialization;

public class Reservation
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
    public double TotalCost { get; set; }

    [JsonPropertyName("passengers")]
    public List<Passenger> Passengers { get; set; }

    public Reservation(int id, string reservationCode, string reservationDate, string flightNumber, int userId, double totalCost, List<Passenger> passengers)
    {
        Id = id;
        ReservationCode = reservationCode;
        ReservationDate = reservationDate;
        FlightNumber = flightNumber;
        UserId = userId;
        TotalCost = totalCost;
        Passengers = passengers;
    }
}

