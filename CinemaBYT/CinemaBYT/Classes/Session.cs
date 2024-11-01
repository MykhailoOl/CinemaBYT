using System.Xml;
using System.Xml.Serialization;
using CinemaBYT.Exceptions;

public class Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall, List<Ticket> tickets)
{
    public TimeSpan Duration { get; set; } = duration;
    public DateTime TimeStart { get; set; } = timeStart;
    public decimal Income { get; set; } = income;
    public Movie Movie { get; set; } = movie;
    public Hall Hall { get; set; } = hall;
    public List<Ticket> Tickets { get; set; } = tickets;
    public History? History { get; set; }

    public Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall, List<Ticket> tickets, History? history) : this(duration, timeStart, income, movie, hall, tickets)
    {
        History = history;
    }

    public bool CheckAvailability()
    {
        if (Hall == null)
        {
            throw new SessionException("The session's hall is not initialized.");
        }

        if (Tickets == null)
        {
            throw new SessionException("The session's tickets are not initialized.");
        }

        if (Tickets.Count==0)
        {
            throw new SessionException("The session's tickets number cannot be 0.");
        }
        return Hall.NumberOfSeats > Tickets.Count;
    }
}