public class Session
{
    public TimeSpan Duration { get; set; }
    public DateTime TimeStart { get; set; }
    public decimal Income { get; set; }
    public Movie Movie { get; set; }
    public Hall Hall { get; set; }
    public List<Ticket> Tickets { get; set; }

    public Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall, List<Ticket> tickets)
    {
        Duration = duration;
        TimeStart = timeStart;
        Income = income;
        Movie = movie;
        Hall = hall;
        Tickets = tickets;
    }
    
    public bool checkAvailability()
    {
        return (Hall.NumberOfSeats > Tickets.Capacity);
    }
}