public class Seat
{
    
    public int SeatNo { get; set; }
    public bool IsVIP { get; set; }
    public bool IsAvailable { get; set; }

    public Seat(int seatNo, bool isVIP, bool isAvailable)
    {
        SeatNo = seatNo;
        IsVIP = isVIP;
        IsAvailable = isAvailable;
    }
    
    public bool ReserveSeat() {
        if (IsAvailable) {
            IsAvailable = false;
            return true;
        }
        return false;
    }
}