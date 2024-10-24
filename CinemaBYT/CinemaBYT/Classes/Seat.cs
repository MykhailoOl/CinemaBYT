using CinemaBYT.Exceptions;

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
    
    public bool ReserveSeat()
    {
        if (!IsAvailable)
        {
            throw new SeatReservationException("Seat is already reserved.");
        }
        IsAvailable = false;
        return true;
    }
}