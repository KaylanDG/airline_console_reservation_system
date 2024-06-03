public class ReservationAccess : JsonHandler<ReservationModel>, IJsonHandler<ReservationModel>
{
    public ReservationAccess()
    {
        path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));
    }
}
