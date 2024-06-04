public class PlaneAccess : JsonHandler<PlaneModel>, IJsonHandler<PlaneModel>
{
    public PlaneAccess()
    {
        path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/planes.json"));
    }
}