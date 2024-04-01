using System.Text.Json.Serialization;
public class FlightModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("flight_numer")]
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
    public PlaneModel Plane { get; set; }

    public FlightModel(int id, string flightNumber, string from, string destination, string departureTime, string flightDuration, string arrivalTime, PlaneModel plane)
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

    public List<ReservationModel> GetFlightReservations()
    {
        List<ReservationModel> flightReservations = new List<ReservationModel>();

        foreach (var reservation in ReservationAccess.LoadAll())
        {
            if (reservation.Flight.Id == Id)
            {
                flightReservations.Add(reservation);
            }
        }

        return flightReservations;
    }

    public List<SeatModel> GetFlightSeats()
    {
        List<SeatModel> flightSeats = Plane.GetPlaneSeats();
        List<ReservationModel> flightReservations = GetFlightReservations();

        foreach (SeatModel seat in flightSeats)
        {
            bool reserved = flightReservations.Any(reservation => reservation.Passengers.Any(passenger => passenger.SeatNumber == seat.SeatNumber));

            if (reserved)
            {
                seat.IsReserved = true;
            }
        }

        return flightSeats;
    }

    public bool IsSeatReserved(string seatNumber)
    {
        List<SeatModel> flightSeats = GetFlightSeats();
        return flightSeats.Any(seat => seat.SeatNumber == seatNumber && seat.IsReserved);
    }
}