public class SeatModel
{
    public string SeatNumber;
    public string SeatType;
    public bool IsReserved;
    public int PricePerMinute;

    public SeatModel(string seatNum, string seatType, int pricePerMin)
    {
        SeatNumber = seatNum;
        SeatType = seatType;
        PricePerMinute = pricePerMin;
    }

    public override string ToString()
    {
        string seat = $"╔════╗\n║{SeatNumber}║\n╚════╝";
        return seat;
    }
}