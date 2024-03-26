using System.Text.Json.Serialization;

public class Passenger
{
    [JsonPropertyName("passenger_id")]
    public int PassengerID { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("seat_number")]
    public string SeatNumber { get; set; }

    public Passenger(int passengerID, string fullName, string seatNumber)
    {
        PassengerID = passengerID;
        FullName = fullName;
        SeatNumber = seatNumber;
    }
}