using System.Text.Json.Serialization;

public class Flight
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("flight_numer")] // Corrected property name
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

    public Flight(int id, string flightNumber, string from, string destination, string departureTime, string flightDuration, string arrivalTime, Plane plane)
    {
        Id = id;
        FlightNumber = flightNumber;
        From = from;
        Destination = destination;
        DepartureTime = departureTime;
        FlightDuration = flightDuration;
        ArrivalTime = arrivalTime;
        Plane = plane;
    }
}
