public class Session
{
    public TimeSpan Duration { get; set; }
    public DateTime TimeStart { get; set; }
    public decimal Income { get; set; }
    public Movie Movie { get; set; }
    public Hall Hall { get; set; }

    public Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall)
    {
        Duration = duration;
        TimeStart = timeStart;
        Income = income;
        Movie = movie;
        Hall = hall;
    }
}