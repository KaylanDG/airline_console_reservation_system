public class FlightsAccess : JsonHandler<FlightModel>, IJsonHandler<FlightModel>
{
    public FlightsAccess()
    {
        path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/flights.json"));
    }
}
