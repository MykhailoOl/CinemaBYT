using CinemaBYT.Exceptions;
using CinemaBYT;
using System.Diagnostics.CodeAnalysis;

public class Seat
{
    private int _seatNo;
    private bool _isVIP;
    private bool _isAvailable = true;
    private Ticket? _ticket;
    private Hall _hall;

    [DisallowNull]
    public int SeatNo
    {
        get => _seatNo;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Seat number must be positive.", nameof(SeatNo));
            }
            _seatNo = value;
        }
    }

    public Hall Hall => _hall;

    [DisallowNull]
    public bool IsVIP
    {
        get => _isVIP;
        set => _isVIP = value;
    }

    [DisallowNull]
    public bool IsAvailable
    {
        get => _isAvailable;
        private set => _isAvailable = value;
    }

    [AllowNull]
    public Ticket? Ticket
    {
        get => _ticket;
        private set => _ticket = value;
    }
    public Seat(int seatNo, bool isVIP, Hall hall, bool isAvailable = true)
    {
        SeatNo = seatNo;
        IsVIP = isVIP;
        _hall = hall; 
        IsAvailable = isAvailable;
    }

    public bool ReserveSeat(Ticket ticket)
    {
        if (ticket == null)
        {
            throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
        }
        if (!IsAvailable)
        {
            throw new SeatReservationException("Seat is already reserved.");
        }

        Ticket = ticket;
        IsAvailable = false;
        return true;
    }

    public bool ReleaseSeat()
    {
        if (IsAvailable)
        {
            throw new SeatReservationException("Seat is already available.");
        }

        Ticket = null;
        IsAvailable = true;
        return true;
    }

    //public override string ToString()
    //{
    //    return $"Seat {SeatNo} - {(IsVIP ? "VIP" : "Standard")}, " +
    //           $"{(IsAvailable ? "Available" : "Reserved")} in Hall {_hall.HallNumber}";
    //}

    public override bool Equals(object obj)
    {
        if (obj is Seat other)
        {
            return SeatNo == other.SeatNo &&
                   IsVIP == other.IsVIP &&
                   IsAvailable == other.IsAvailable;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hashCode = SeatNo.GetHashCode();
        hashCode = (hashCode * 397) ^ IsVIP.GetHashCode();
        hashCode = (hashCode * 397) ^ IsAvailable.GetHashCode();
        hashCode = (hashCode * 397) ^ _hall.GetHashCode();  
        return hashCode;
    }

    public void addHall(Hall hall) {
        _hall = hall;
    }
    public void addTicket(Ticket ticket) {
        _ticket = ticket;
    }
    public void deleteHall() {
        _hall = null;
    }
    public void deleteTicket()
    {
        if (_ticket == null)
        {
            throw new ArgumentNullException("No seat");
        }
        _ticket.deleteSeat();
        _ticket = null;
    }
}

