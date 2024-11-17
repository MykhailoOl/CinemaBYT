using CinemaBYT;
using CinemaBYT.Classes;
using NUnit.Framework.Constraints;

namespace CinemaBYT_Tests;

public class LoadInfoTest
{
    private string testFilePath;
    private LoadInfo serializeInfo;
    private List<OwnsLoyaltyCard> _loyaltyOwners;
    private List<Seat> _seats;
    private List<Movie> _movies;
    private List<Support> _supportStaff;
    private List<Manager> _managers;
    private List<Person> _people;
    private List<Hall> _halls;
    private List<Ticket> _tickets;
    private List<Session> _sessions;
    private List<Cinema> _cinemas;
    private List<History> _histories;
    private List<Comment> _comments;
    private List<Payment> _payments;

    [SetUp]
    public void Setup()
    {
        serializeInfo = new LoadInfo();
        testFilePath = "test.xml";
        
        _loyaltyOwners = new List<OwnsLoyaltyCard>();
        _seats = new List<Seat>();
        _movies = new List<Movie>();
        _supportStaff = new List<Support>();
        _managers = new List<Manager>();
        _people = new List<Person>();
        _halls = new List<Hall>();
        _tickets = new List<Ticket>();
        _sessions = new List<Session>();
        _cinemas = new List<Cinema>();
        _histories = new List<History>();
        _comments = new List<Comment>();
        _payments = new List<Payment>();
    }

    [Test]
    public void SerializationDeserialization_Validation()
    {
        serializeInfo.People.Add(new Manager(DateTime.Today,12,"superboss","joe", "afasdf", DateTime.Today, "12312312312"));
        serializeInfo.Cinemas.Add(new Cinema("cinema","kyiv","ukraine","132132","12"));
        List<string> genres = new List<string>();
        genres.Add("adventure");
        Movie movie = new Movie("movie", DateTime.Today, 12, genres);
        serializeInfo.Movies.Add(movie);
        serializeInfo.Comments.Add(new Comment("good",DateTime.Today,movie,serializeInfo.People[0]));
        
        serializeInfo.Histories.Add(new History(serializeInfo.People[0]));
        
        List <Seat> seats = new List<Seat>();
        for (int i = 1; i <= 25; i++)
        {
            seats.Add(new Seat(i, false));
            serializeInfo.Seats.Add(seats[i-1]);
        }

        Hall hall = new Hall(1, 25, seats);
        serializeInfo.Halls.Add(hall);
        serializeInfo.Managers.Add(new Manager(DateTime.Today, 5,"man","joe","sfdsf",DateTime.Today, "12313211111"));
        serializeInfo.People.Add(serializeInfo.Managers[0]);

        serializeInfo.Payments.Add(new Payment(PaymentType.Blik,DateTime.Today));
        
        serializeInfo.Sessions.Add(new Session(TimeSpan.Zero, DateTime.Today, 33,movie,hall,new List<Ticket>()));
        serializeInfo.Tickets.Add(new Ticket(serializeInfo.Seats[0].SeatNo, 10, DateTime.Today, TicketType.Adult, serializeInfo.Sessions[0], serializeInfo.Seats[0], serializeInfo.People[0]));
        serializeInfo.SupportStaff.Add(new Support(DateTime.Today, 33,"joe","adfafa",DateTime.Today, "12312312312","4"));
        
        serializeInfo.SerializeToXml(testFilePath);
        
        // Transfer the data from serializeInfo to custom lists
        _loyaltyOwners.AddRange(serializeInfo.LoyaltyOwners);
        _seats.AddRange(serializeInfo.Seats);
        _movies.AddRange(serializeInfo.Movies);
        _supportStaff.AddRange(serializeInfo.SupportStaff);
        _managers.AddRange(serializeInfo.Managers);
        _people.AddRange(serializeInfo.People);
        _halls.AddRange(serializeInfo.Halls);
        _tickets.AddRange(serializeInfo.Tickets);
        _sessions.AddRange(serializeInfo.Sessions);
        _cinemas.AddRange(serializeInfo.Cinemas);
        _histories.AddRange(serializeInfo.Histories);
        _comments.AddRange(serializeInfo.Comments);
        _payments.AddRange(serializeInfo.Payments);

        // Clear the serializeInfo lists to prepare for the next test
        serializeInfo.LoyaltyOwners.Clear();
        serializeInfo.Seats.Clear();
        serializeInfo.Movies.Clear();
        serializeInfo.SupportStaff.Clear();
        serializeInfo.Managers.Clear();
        serializeInfo.People.Clear();
        serializeInfo.Halls.Clear();
        serializeInfo.Tickets.Clear();
        serializeInfo.Sessions.Clear();
        serializeInfo.Cinemas.Clear();
        serializeInfo.Histories.Clear();
        serializeInfo.Comments.Clear();
        serializeInfo.Payments.Clear();
        
        serializeInfo.LoadFromXml(testFilePath);
        
        
   // Assert the contents of the lists
    Assert.AreEqual(_loyaltyOwners.Count, serializeInfo.LoyaltyOwners.Count, "Loyalty owners count mismatch");
    CollectionAssert.AreEqual(_loyaltyOwners, serializeInfo.LoyaltyOwners, "Loyalty owners content mismatch");

    Assert.AreEqual(_seats.Count, serializeInfo.Seats.Count, "Seats count mismatch");
    CollectionAssert.AreEqual(_seats, serializeInfo.Seats, "Seats content mismatch");

    Assert.AreEqual(_movies.Count, serializeInfo.Movies.Count, "Movies count mismatch");
    CollectionAssert.AreEqual(_movies, serializeInfo.Movies, "Movies content mismatch");

    Assert.AreEqual(_supportStaff.Count, serializeInfo.SupportStaff.Count, "Support staff count mismatch");
    
    CollectionAssert.AreEqual(_supportStaff, serializeInfo.SupportStaff, "Support staff content mismatch");
   

    Assert.AreEqual(_managers.Count, serializeInfo.Managers.Count, "Managers count mismatch");
        
        CollectionAssert.AreEqual(_managers, serializeInfo.Managers, "Managers content mismatch");
        

        Assert.AreEqual(_people.Count, serializeInfo.People.Count, "People count mismatch");

    Assert.AreEqual(_halls.Count, serializeInfo.Halls.Count, "Halls count mismatch");
    CollectionAssert.AreEqual(_halls, serializeInfo.Halls, "Halls content mismatch");

    Assert.AreEqual(_tickets.Count, serializeInfo.Tickets.Count, "Tickets count mismatch");
    /*
    CollectionAssert.AreEqual(_tickets, serializeInfo.Tickets, "Tickets content mismatch");
    */

    Assert.AreEqual(_sessions.Count, serializeInfo.Sessions.Count, "Sessions count mismatch");
    CollectionAssert.AreEqual(_sessions, serializeInfo.Sessions, "Sessions content mismatch");

    Assert.AreEqual(_cinemas.Count, serializeInfo.Cinemas.Count, "Cinemas count mismatch");
    CollectionAssert.AreEqual(_cinemas, serializeInfo.Cinemas, "Cinemas content mismatch");

    Assert.AreEqual(_histories.Count, serializeInfo.Histories.Count, "Histories count mismatch");
    /*
    CollectionAssert.AreEqual(_histories, serializeInfo.Histories, "Histories content mismatch");
    */

    Assert.AreEqual(_comments.Count, serializeInfo.Comments.Count, "Comments count mismatch");
    /*
    CollectionAssert.AreEqual(_comments, serializeInfo.Comments, "Comments content mismatch");
    */

    Assert.AreEqual(_payments.Count, serializeInfo.Payments.Count, "Payments count mismatch");
    CollectionAssert.AreEqual(_payments, serializeInfo.Payments, "Payments content mismatch"); }
}
