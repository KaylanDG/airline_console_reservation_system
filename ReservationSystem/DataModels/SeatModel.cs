public class SeatModel
{
    public string SeatNumber;
    public string SeatType;
    public int PricePerMinute;
    public bool IsReserved;
    public bool IsSelected;


    public SeatModel(string seatNum, string seatType, int pricePerMin)
    {
        SeatNumber = seatNum;
        SeatType = seatType;
        PricePerMinute = pricePerMin;
    }

}