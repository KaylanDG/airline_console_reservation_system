using System.Text.Json.Serialization;

public class ServiceModel
{
    [JsonPropertyName("service_type")]
    public string ServiceType { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("cost")]
    public double Cost { get; set; }

    public ServiceModel(string serviceType, int quantity, double cost)
    {
        ServiceType = serviceType;
        Quantity = quantity;
        Cost = cost;
    }
}