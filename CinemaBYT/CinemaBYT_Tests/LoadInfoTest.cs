using CinemaBYT;
using CinemaBYT.Classes;
using NUnit.Framework.Constraints;

namespace CinemaBYT_Tests;

public class LoadInfoTest
{
    private string testFilePath;
    private LoadInfo serializeInfo;
    private LoadInfo deserializeInfo;


    [SetUp]
    public void Setup()
    {
        serializeInfo = new LoadInfo();
        deserializeInfo = new LoadInfo();
        testFilePath = "test.xml";
    }

    [Test]
    public void SerializationDeserialization_Validation()
    {
        serializeInfo.people.Add(new Buyer("joe", "afasdf", DateTime.Today, "12312312312"));
        serializeInfo.cinemas.Add(new Cinema("cinema","kyiv","ukraine","132132","12"));
        List<string> genres = new List<string>();
        genres.Add("adventure");
        Movie movie = new Movie("movie", DateTime.Now, 12, genres);
        serializeInfo.movies.Add(movie);
        serializeInfo.comments.Add(new Comment("good",DateTime.Now,movie,serializeInfo.people[0]));
        
        serializeInfo.histories.Add(new History(serializeInfo.people[0]));
        
        List <Seat> seats = new List<Seat>();
        for (int i = 1; i <= 25; i++)
        {
            seats.Add(new Seat(i, false));
            serializeInfo.seats.Add(seats[i-1]);
        }

        Hall hall = new Hall(1, 25, seats);
        serializeInfo.halls.Add(hall);
        serializeInfo.managers.Add(new Manager(DateTime.Today, 5,"man","joe","sfdsf",DateTime.Now, "12313211111"));
    

   
        serializeInfo.payments.Add(new Payment(PaymentType.Blik,DateTime.Today));
  
    
        
        serializeInfo.sessions.Add(new Session(TimeSpan.Zero, DateTime.Today, 33,movie,hall,new List<Ticket>()));
        serializeInfo.tickets.Add(new Ticket(serializeInfo.seats[0].SeatNo, 10, DateTime.Today, TicketType.Adult, serializeInfo.sessions[0], serializeInfo.seats[0], serializeInfo.people[0]));
        serializeInfo.supportStaff.Add(new Support(DateTime.Today, 33,"joe","adfafa",DateTime.Now, "12312312312","4"));
        
        serializeInfo.SerializeToXml("test.xml");
        
        
        deserializeInfo.LoadFromXml("test.xml");
   
        // Verify that each list in deserializeInfo has the same count as in serializeInfo
        Assert.AreEqual(serializeInfo.cinemas.Count, deserializeInfo.cinemas.Count, "Cinemas count mismatch");
        Assert.AreEqual(serializeInfo.comments.Count, deserializeInfo.comments.Count, "Comments count mismatch");
        Assert.AreEqual(serializeInfo.histories.Count, deserializeInfo.histories.Count, "Histories count mismatch");
        Assert.AreEqual(serializeInfo.halls.Count, deserializeInfo.halls.Count, "Halls count mismatch");
        Assert.AreEqual(serializeInfo.managers.Count, deserializeInfo.managers.Count, "Managers count mismatch");
        Assert.AreEqual(serializeInfo.movies.Count, deserializeInfo.movies.Count, "Movies cou   nt mismatch");
        Assert.AreEqual(serializeInfo.payments.Count, deserializeInfo.payments.Count, "Payments count mismatch");
        //Assert.AreEqual(serializeInfo.people.Count, deserializeInfo.people.Count, "People count mismatch");
        Assert.AreEqual(serializeInfo.seats.Count, deserializeInfo.seats.Count, "Seats count mismatch");
        Assert.AreEqual(serializeInfo.sessions.Count, deserializeInfo.sessions.Count, "Sessions count mismatch");
        //Assert.AreEqual(serializeInfo.tickets.Count, deserializeInfo.tickets.Count, "Tickets count mismatch");
        Assert.AreEqual(serializeInfo.supportStaff.Count, deserializeInfo.supportStaff.Count, "Support staff count mismatch");

    }
}
