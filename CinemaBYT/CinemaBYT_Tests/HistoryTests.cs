using CinemaBYT;

namespace CinemaBYT_Tests;

    public class HistoryTests
    {
        private Person _person;
        private Session _session1;
        private Session _session2;

        [SetUp]
        public void Setup()
        {
            // Create a Person object and some Session objects to be used in tests
            _person = new Buyer("John Doe", "john.doe@example.com", new DateTime(1985, 5, 15), "12345678900");
            _session1 = new Session();
            _session2 = new Session();
        }

        // Test 1: Constructor with valid parameters
        [Test]
        public void Constructor_ValidParameters_ShouldCreateHistoryObject()
        {
            // Arrange
            var sessions = new List<Session> { _session1, _session2 };

            // Act
            var history = new History(sessions, _person);

            // Assert
            Assert.AreEqual(sessions, history.ListOfSessions);
            Assert.AreEqual(_person, history.Person);
        }

        // Test 2: Constructor with null person should throw exception
        [Test]
        public void Constructor_NullPerson_ShouldThrowArgumentNullException()
        {
            // Arrange
            List<Session> sessions = new List<Session> { _session1, _session2 };

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new History(sessions, null));
            Assert.AreEqual("Person cannot be null. (Parameter 'Person')", exception.Message);
        }

        // Test 3: Constructor with empty sessions list
        [Test]
        public void Constructor_EmptySessionsList_ShouldInitializeEmptySessionList()
        {
            // Act
            var history = new History(new List<Session>(), _person);

            // Assert
            Assert.NotNull(history.ListOfSessions); // ListOfSessions should be initialized
            Assert.AreEqual(0, history.ListOfSessions.Count); // The list should be empty
        }

        // Test 4: Set ListOfSessions as null (allowed by the class)
        [Test]
        public void SetListOfSessions_Null_ShouldAllowNull()
        {
            // Arrange
            var history = new History();

            // Act
            history.ListOfSessions = null;

            // Assert
            Assert.IsNull(history.ListOfSessions); // Should be null as it's allowed
        }

        // Test 5: Add sessions after initialization
        [Test]
        public void AddSessions_AfterInitialization_ShouldAddCorrectly()
        {
            // Arrange
            var history = new History();

            // Act
            history.ListOfSessions = new List<Session> { _session1 };
            history.ListOfSessions.Add(_session2);

            // Assert
            Assert.AreEqual(2, history.ListOfSessions.Count); // The list should contain 2 sessions
            Assert.Contains(_session2, history.ListOfSessions); // The second session should be in the list
        }

        // Test 6: Setting valid ListOfSessions should work
        [Test]
        public void SetListOfSessions_ValidList_ShouldSetListCorrectly()
        {
            // Arrange
            var history = new History();

            // Act
            history.ListOfSessions = new List<Session> { _session1, _session2 };

            // Assert
            Assert.AreEqual(2, history.ListOfSessions.Count);
            Assert.AreEqual(_session1, history.ListOfSessions[0]);
            Assert.AreEqual(_session2, history.ListOfSessions[1]);
        }

        // Test 7: Setting Person to null should throw exception
        [Test]
        public void SetPerson_NullPerson_ShouldThrowArgumentNullException()
        {
            // Arrange
            var history = new History();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => history.Person = null);
            Assert.AreEqual("Person cannot be null. (Parameter 'Person')", exception.Message);
        }

        // Test 8: Setting Person to a valid value
        [Test]
        public void SetPerson_ValidPerson_ShouldSetPersonCorrectly()
        {
            // Arrange
            var history = new History();
            var newPerson = new Buyer("Jane Doe", "jane.doe@example.com", new DateTime(1990, 7, 25), "09807654321");

            // Act
            history.Person = newPerson;

            // Assert
            Assert.AreEqual(newPerson, history.Person); // The person should now be set to the new person
        }

        // Test 9: History with no sessions should still be valid
        [Test]
        public void History_NoSessions_ShouldHandleEmptyListCorrectly()
        {
            // Arrange
            var history = new History(new List<Session>(), _person);

            // Act
            var result = history.ListOfSessions;

            // Assert
            Assert.NotNull(result); // The list of sessions should be initialized
            Assert.AreEqual(0, result.Count); // The list should be empty
        }

        // Test 10: Ensure ListOfSessions can be set multiple times
        [Test]
        public void SetListOfSessions_MultipleTimes_ShouldUpdateCorrectly()
        {
            // Arrange
            var history = new History();
            var newSession = new Session();
                // Act
            history.ListOfSessions = new List<Session> { _session1, _session2 };
            history.ListOfSessions.Add(newSession);

            // Assert
            Assert.AreEqual(3, history.ListOfSessions.Count); // The list should now have 3 sessions
            Assert.Contains(newSession, history.ListOfSessions); // The new session should be in the list
        }

        // Test 11: Ensure `Person` property works correctly after initialization
        [Test]
        public void SetPerson_AfterInitialization_ShouldUpdatePerson()
        {
            // Arrange
            var history = new History(new List<Session>(), _person);
            var updatedPerson = new Buyer("Alice", "alice@example.com", new DateTime(1985, 10, 1), "09876543210");

            // Act
            history.Person = updatedPerson;

            // Assert
            Assert.AreEqual(updatedPerson, history.Person);
        }
    }