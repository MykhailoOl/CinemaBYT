using CinemaBYT;
using CinemaBYT.Exceptions;

namespace CinemaBYT_Tests;

public class SessionTests
    {
        private Movie _movie;
        private Hall _hall;
        private List<Ticket> _tickets;

        [SetUp]
        public void Setup()
        {
            _movie = new Movie("Test Movie", DateTime.Now.AddDays(-1), 120, new List<string> { "Action" });
            _hall = new Hall(); // Assuming Hall constructor takes HallNumber and NumberOfSeats
            _tickets = new List<Ticket>(); // Assuming Ticket is defined elsewhere
        }

        // Test for Constructor with Null Movie
        [Test]
        public void Constructor_NullMovie_ShouldThrowArgumentNullException()
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(120);
            var timeStart = DateTime.Now.AddHours(2);
            decimal income = 0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new Session(duration, timeStart, income, null, _hall, _tickets));
            Assert.AreEqual("Movie cannot be null. (Parameter 'Movie')", exception.Message);
        }

        // Test for Constructor with Null Hall
        [Test]
        public void Constructor_NullHall_ShouldThrowArgumentNullException()
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(120);
            var timeStart = DateTime.Now.AddHours(2);
            decimal income = 0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new Session(duration, timeStart, income, _movie, null, _tickets));
            Assert.AreEqual("Hall cannot be null. (Parameter 'Hall')", exception.Message);
        }

        // Test for Constructor with Valid Parameters
        [Test]
        public void Constructor_ValidParameters_ShouldCreateSession()
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(120);
            var timeStart = DateTime.Now.AddHours(2);
            decimal income = 0;

            // Act
            var session = new Session(duration, timeStart, income, _movie, _hall, _tickets);

            // Assert
            Assert.AreEqual(duration, session.Duration);
            Assert.AreEqual(timeStart, session.TimeStart);
            Assert.AreEqual(income, session.Income);
            Assert.AreEqual(_movie, session.Movie);
            Assert.AreEqual(_hall, session.Hall);
            Assert.IsNotNull(session.Tickets);
            Assert.AreEqual(0, session.Tickets.Count);
        }

        // Test for Duration Property
        [Test]
        public void Duration_SetValidValue_ShouldUpdateDuration()
        {
            // Arrange
            var session = new Session(TimeSpan.FromMinutes(120), DateTime.Now.AddHours(1), 0m, _movie, _hall, _tickets);
            var newDuration = TimeSpan.FromMinutes(90);

            // Act
            session.Duration = newDuration;

            // Assert
            Assert.AreEqual(newDuration, session.Duration);
        }

        // Test for TimeStart Property
        [Test]
        public void TimeStart_SetValidValue_ShouldUpdateTimeStart()
        {
            // Arrange
            var session = new Session(TimeSpan.FromMinutes(120), DateTime.Now.AddHours(1), 0m, _movie, _hall, _tickets);
            var newTimeStart = DateTime.Now.AddHours(3);

            // Act
            session.TimeStart = newTimeStart;

            // Assert
            Assert.AreEqual(newTimeStart, session.TimeStart);
        }

        // Test for Income Property
        [Test]
        public void AddIncome_NegativeAmount_ShouldThrowArgumentException()
        {
            // Arrange
            var session = new Session(TimeSpan.FromMinutes(120), DateTime.Now.AddHours(1), 0m, _movie, _hall, _tickets);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => session.AddIncome(-100));
            Assert.AreEqual("Income amount cannot be negative. (Parameter 'amount')", exception.Message);
        }

        [Test]
        public void AddIncome_ValidAmount_ShouldIncreaseIncome()
        {
            // Arrange
            var session = new Session(TimeSpan.FromMinutes(120), DateTime.Now.AddHours(1), 0m, _movie, _hall, _tickets);
            decimal amountToAdd = 150;

            // Act
            session.AddIncome(amountToAdd);

            // Assert
            Assert.AreEqual(amountToAdd, session.Income);
        }
        
        // Test for ToString
        [Test]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var session = new Session(TimeSpan.FromMinutes(120), DateTime.Now.AddHours(1), 0m, _movie, _hall, _tickets);

            // Act
            var result = session.ToString();

            // Assert
            Assert.IsTrue(result.Contains(_movie.Name));
            Assert.IsTrue(result.Contains(_hall.HallNumber.ToString()));
        }
    }