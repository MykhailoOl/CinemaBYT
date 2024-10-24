using System.Xml;
using System.Xml.Serialization;

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
    public List<Ticket> load()
    {
        Tickets = new List<Ticket>();
        string path = "tickets.xml";    
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch (FileNotFoundException) {
            return null;
        }
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Ticket>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                Tickets = (List<Ticket>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        return Tickets;
    }
}