using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


class Overview
{
    static void Main(string[] args)
    {
        List<Flight> flights = OverviewAccess.LoadAllFlights();
        OverviewLogic.PrintFlightOverview(flights);
    }
}


public class Flight
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("flight_numer")]
    public string FlightNumber { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("destination")]
    public string Destination { get; set; }

    [JsonPropertyName("departure_date")]
    public string DepartureDate { get; set; }

    [JsonPropertyName("departure_time")]
    public string DepartureTime { get; set; }

    [JsonPropertyName("flight_duration")]
    public string FlightDuration { get; set; }

    [JsonPropertyName("arrival_date")]
    public string ArrivalDate { get; set; }

    [JsonPropertyName("arrival_time")]
    public string ArrivalTime { get; set; }

    [JsonPropertyName("plane")]
    public Plane Plane { get; set; }
}


public class Plane
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("airline")]
    public string Airline { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("passengers")]
    public int Passengers { get; set; }
}
