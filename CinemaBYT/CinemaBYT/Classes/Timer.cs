using CinemaBYT;

public class Timer
{
    private DateTime dateTimeStart;
    private DateTime dateTimeEnd;
    private bool isAvailable = true;
    private Seat _seat;
    private Hall _hall;

    public DateTime DateTimeStart
    {
        get { return dateTimeStart; }
        set { dateTimeStart = value; }
    }

    public DateTime DateTimeEnd
    {
        get { return dateTimeEnd; }
        set { dateTimeEnd = value; }
    }

    public bool IsAvailable
    {
        get { return isAvailable; }
        set { isAvailable = value; }
    }

    public Seat Seat
    {
        get { return _seat; }
        private set { _seat = value; }
    }

    public Hall Hall
    {
        get { return _hall; }
        private set { _hall = value; }
    }

    public Timer(DateTime start, DateTime end, bool available = true)
    {
        if (dateTimeStart >= dateTimeEnd)
                 throw new ArgumentException("Start time must be earlier than end time.");
        dateTimeStart = start;
        dateTimeEnd = end;
        isAvailable = available;
    }

    public Timer(DateTime dateTimeStart, DateTime dateTimeEnd, bool isAvailable, Seat seat, Hall hall)
    {
        if (seat == null)
            throw new ArgumentNullException(nameof(seat), "Seat cannot be null.");

        if (hall == null)
            throw new ArgumentNullException(nameof(hall), "Hall cannot be null.");
        
        this.dateTimeStart = dateTimeStart;
        this.dateTimeEnd = dateTimeEnd;
        this.isAvailable = isAvailable;
        _seat = seat;
        _hall = hall;
    }


    public void AddHall(Hall hall)
    {
        if (hall == null)
            throw new ArgumentNullException(nameof(hall), "Hall cannot be null.");
        if (_hall == hall)
            throw new InvalidOperationException("This timer is already associated with the specified hall.");

        _hall = hall;
        if (!hall.GetTimers().Contains(this))
        {
            hall.AddTimer(this);
        }
    }

    public void RemoveHall()
    {
        if (_hall == null)
            throw new InvalidOperationException("No hall is currently associated with this timer.");

        Hall oldHall = _hall;
        _hall = null;

        if (oldHall.GetTimers().Contains(this))
        {
            oldHall.RemoveTimer(this);
        }
    }

    public void AddSeat(Seat seat)
    {
        if (seat == null)
            throw new ArgumentNullException(nameof(seat), "Seat cannot be null.");

        if (_seat == seat)
            throw new InvalidOperationException("This timer is already associated with the specified seat.");

        _seat = seat;

        if (!seat.GetTimers().Contains(this))
        {
            seat.AddTimer(this);
        }
    }

    public void RemoveSeat()
    {
        if (_seat == null)
            throw new InvalidOperationException("No seat is currently associated with this timer.");

        Seat oldSeat = _seat;
        _seat = null;

        if (oldSeat.GetTimers().Contains(this))
        {
            oldSeat.RemoveTimer(this);
        }
    }

}
