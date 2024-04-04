using System.Text.Json.Serialization;
public class Flight
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("flight_number")]
    public string FlightNumber { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("destination")]
    public string Destination { get; set; }

    [JsonPropertyName("departure_time")]
    public string DepartureTime { get; set; }

    [JsonPropertyName("flight_duration")]
    public string FlightDuration { get; set; }

    [JsonPropertyName("arrival_time")]
    public string ArrivalTime { get; set; }

    [JsonPropertyName("plane")]
    public Plane Plane { get; set; }



    public Flight(int id, string flightnumber, string from, string destination, string departure_time, string flight_duration, string arrival_time, Plane plane)
    {
        Id = id;
        FlightNumber = flightnumber;
        From = from;
        Destination = destination;
        DepartureTime = departure_time;
        FlightDuration = flight_duration;
        ArrivalTime = arrival_time;
        Plane = plane;
    }
}