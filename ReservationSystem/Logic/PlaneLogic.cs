using System.Globalization;

public class PlaneLogic
{
    private List<PlaneModel> _planes;
    private PlaneAccess _planeAcces = new PlaneAccess();
    public PlaneLogic()
    {
        _planes = _planeAcces.LoadAll();
    }

    public List<PlaneModel> GetPlanes()
    {
        return _planes;
    }

    public List<SeatModel> GetPlaneSeats(int planeid)
    {
        FlightLogic _flightlogic = new FlightLogic();

        PlaneModel plane = _flightlogic.GetPlaneByID(planeid);

        List<SeatModel> planeSeats = new List<SeatModel>();

        int totalSeats = plane.FirstClassSeats + plane.EconomySeats;

        int seatsPerRow = (plane.Name == "Boeing 737") ? 6 : 9;

        int totalRows = totalSeats / seatsPerRow;
        int firstClassRows = plane.FirstClassSeats / seatsPerRow;

        for (int i = 0; i < totalRows; i++)
        {
            int seatLetterAscii = 65;

            for (int j = 0; j < seatsPerRow; j++)
            {
                char seatLetter = (char)seatLetterAscii;
                string rowNumber = (i + 1 < 10) ? $"0{i + 1}" : $"{i + 1}";

                string seatNumber = $"{seatLetter}-{rowNumber}";

                if (i < firstClassRows)
                {
                    planeSeats.Add(new SeatModel(seatNumber, "First Class", 5));
                }
                else
                {
                    planeSeats.Add(new SeatModel(seatNumber, "Economy Class", 4));
                }

                seatLetterAscii++;
            }
        }

        return planeSeats;
    }

    public bool DoesSeatExist(string seatNumber, int planeid)
    {
        List<SeatModel> planeSeats = GetPlaneSeats(planeid);
        return planeSeats.Any(seat => seat.SeatNumber == seatNumber);
    }

    public bool doesPlaneExist(int planeId)
    {
        return _planes.Any(plane => plane.Id == planeId);
    }

    public bool IsPlaneAvailable(int planeId, string date, int duration, int flightAmount = 1)
    {
        DateTime departure = DateTime.ParseExact(date, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        DateTime arrival = departure.AddMinutes(duration);
        FlightLogic _flightLogic = new FlightLogic();

        for (int i = 1; i <= flightAmount; i++)
        {
            foreach (FlightModel flight in _flightLogic.GetAvailableFlights())
            {
                if (flight.Plane == planeId)
                {
                    string format = "dd-MM-yyyy HH:mm";
                    DateTime flightDeparture = DateTime.ParseExact(flight.DepartureTime, format, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime flightArrival = DateTime.ParseExact(flight.ArrivalTime, format, System.Globalization.CultureInfo.InvariantCulture);
                    if (departure >= flightDeparture && departure <= flightArrival)
                    {
                        return false;
                    }

                    if (arrival >= flightDeparture && arrival <= flightArrival)
                    {
                        return false;
                    }
                }
            }

            departure = departure.AddDays(1);
            arrival = arrival.AddDays(1);
        }
        return true;
    }
}