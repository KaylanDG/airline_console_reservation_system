using System.Text.Json.Serialization;

public class PassengerModel
{
    [JsonPropertyName("passenger_id")]
    public int PassengerID { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("seat_number")]
    public string SeatNumber { get; set; }

    [JsonPropertyName("additional_services")]
    public List<ServiceModel> AdditionalServices { get; set; }

    public PassengerModel(int passengerID)
    {
        PassengerID = passengerID;
        AdditionalServices = new List<ServiceModel>();
    }
}