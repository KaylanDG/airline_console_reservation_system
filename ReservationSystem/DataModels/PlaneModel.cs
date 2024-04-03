using System.Text.Json.Serialization;

public class PlaneModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("airline")]
    public string Airline { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("economy_seats")]
    public int EconomySeats { get; set; }

    [JsonPropertyName("first_class_seats")]
    public int FirstClassSeats { get; set; }

    public List<SeatModel> GetPlaneSeats()
    {
        List<SeatModel> planeSeats = new List<SeatModel>();

        int totalSeats = FirstClassSeats + EconomySeats;

        int seatsPerRow = (Name == "Boeing 737") ? 6 : 9;

        int totalRows = totalSeats / seatsPerRow;
        int firstClassRows = FirstClassSeats / seatsPerRow;

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

    public bool DoesSeatExist(string seatNumber)
    {
        List<SeatModel> planeSeats = GetPlaneSeats();
        return planeSeats.Any(seat => seat.SeatNumber == seatNumber);
    }
}