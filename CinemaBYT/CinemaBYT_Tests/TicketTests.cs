using CinemaBYT;

namespace CinemaBYT_Tests;

public class TicketTests
    {
        private Session _session;
        private Seat _seat;
        private Person _person;

        [SetUp]
        public void Setup()
        {
            // Initialize objects needed for testing
            _session = new Session(TimeSpan.FromMinutes(120), DateTime.Now.AddHours(1), 0m, new Movie("Test Movie", DateTime.Now, 120, new List<string> { "Action" }), new Hall(), new List<Ticket>());
            /*
            _seat = new Seat(1, false); // Not a VIP seat
            */
            _person = new Buyer("John Doe", "john.doe@example.com", new DateTime(1985, 5, 15), "12345678900");
        }

        // Test for Constructor with Null Session
        [Test]
        public void Constructor_NullSession_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new Ticket(1, 10.00m, DateTime.Now, TicketType.Adult, null, _seat, _person));
            Assert.AreEqual("Session cannot be null. (Parameter 'Session')", exception.Message);
        }

        // Test for Constructor with Null Seat
        [Test]
        public void Constructor_NullSeat_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new Ticket(1, 10.00m, DateTime.Now, TicketType.Adult, _session, null, _person));
            Assert.AreEqual("Seat cannot be null. (Parameter 'Seat')", exception.Message);
        }

        // Test for Constructor with Null Person
        [Test]
        public void Constructor_NullPerson_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new Ticket(1, 10.00m, DateTime.Now, TicketType.Adult, _session, _seat, null));
            Assert.AreEqual("Person cannot be null. (Parameter 'Person')", exception.Message);
        }

        // Test for Constructor with Invalid Seat Number
        [Test]
        public void Constructor_InvalidSeatNumber_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Ticket(0, 10.00m, DateTime.Now, TicketType.Adult, _session, _seat, _person));
            Assert.AreEqual("Seat number must be positive. (Parameter 'SeatNumber')", exception.Message);
        }

        // Test for Constructor with Invalid Price
        [Test]
        public void Constructor_NegativePrice_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Ticket(1, -10.00m, DateTime.Now, TicketType.Adult, _session, _seat, _person));
            Assert.AreEqual("Price cannot be negative. (Parameter 'Price')", exception.Message);
        }

        // Test for Valid Constructor
        [Test]
        public void Constructor_ValidParameters_ShouldCreateTicket()
        {
            // Act
            var ticket = new Ticket(1, 10.00m, DateTime.Now, TicketType.Adult, _session, _seat, _person);

            // Assert
            Assert.AreEqual(1, ticket.SeatNumber);
            Assert.AreEqual(10.00m, ticket.Price);
            Assert.AreEqual(DateTime.Now.Date, ticket.PurchaseDate.Date); // Check only the date part
            Assert.AreEqual(TicketType.Adult, ticket.Type);
            Assert.AreEqual(_session, ticket.Session);
            Assert.AreEqual(_seat, ticket.Seat);
            Assert.AreEqual(_person, ticket.Person);
        }
    }