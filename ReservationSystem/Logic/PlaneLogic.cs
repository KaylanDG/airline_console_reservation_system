public class PlaneLogic
{
    private List<PlaneModel> _planes;

    public PlaneLogic()
    {
        _planes = PlaneAccess.LoadAllPlanes();
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
}
