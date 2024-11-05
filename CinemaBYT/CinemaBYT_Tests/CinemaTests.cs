
using CinemaBYT;

namespace CinemaBYT_Tests
{
    public class CinemaTests
    {
        private Cinema _cinema;

        [SetUp]
        public void Setup()
        {
            // Initialize a common cinema instance for tests
            _cinema = new Cinema("Cinema 1", "City 1", "Country 1", "1234567890", "10:00 AM - 11:00 PM");
        }

        // Test for constructor and property initialization
        [Test]
        public void Constructor_ShouldInitializeProperties_WhenValidArgumentsAreProvided()
        {
            // Assert
            Assert.AreEqual("Cinema 1", _cinema.Name);
            Assert.AreEqual("City 1", _cinema.City);
            Assert.AreEqual("Country 1", _cinema.Country);
            Assert.AreEqual("1234567890", _cinema.ContactPhone);
            Assert.AreEqual("10:00 AM - 11:00 PM", _cinema.OpeningHours);
            Assert.IsNotNull(_cinema.Halls);
            Assert.IsEmpty(_cinema.Halls);
        }

         // Constructor error handling tests
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Cinema(null, "City 1", "Country 1", "1234567890", "10:00 AM - 11:00 PM"));
            Assert.That(exception.Message, Does.Contain("Name cannot be null.").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCityIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Cinema("Cinema 1", null, "Country 1", "1234567890", "10:00 AM - 11:00 PM"));
            Assert.That(exception.Message, Does.Contain("City cannot be null.").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCountryIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Cinema("Cinema 1", "City 1", null, "1234567890", "10:00 AM - 11:00 PM"));
            Assert.That(exception.Message, Does.Contain("Country cannot be null.").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenContactPhoneIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Cinema("Cinema 1", "City 1", "Country 1", null, "10:00 AM - 11:00 PM"));
            Assert.That(exception.Message, Does.Contain("Contact phone cannot be null.").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenOpeningHoursIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Cinema("Cinema 1", "City 1", "Country 1", "1234567890", null));
            Assert.That(exception.Message, Does.Contain("Opening hours cannot be null.").IgnoreCase);
        }

        // Property error handling tests
        [Test]
        public void Name_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _cinema.Name = null);
            Assert.That(exception.Message, Does.Contain("Name cannot be null.").IgnoreCase);
        }

        [Test]
        public void City_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _cinema.City = null);
            Assert.That(exception.Message, Does.Contain("City cannot be null.").IgnoreCase);
        }

        [Test]
        public void Country_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _cinema.Country = null);
            Assert.That(exception.Message, Does.Contain("Country cannot be null.").IgnoreCase);
        }

        [Test]
        public void ContactPhone_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _cinema.ContactPhone = null);
            Assert.That(exception.Message, Does.Contain("Contact phone cannot be null.").IgnoreCase);
        }

        [Test]
        public void OpeningHours_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _cinema.OpeningHours = null);
            Assert.That(exception.Message, Does.Contain("Opening hours cannot be null.").IgnoreCase);
        }

        // Property tests for setting and getting values
        [Test]
        public void Name_SetGet_ShouldWorkCorrectly()
        {
            // Arrange
            var expectedName = "Cinema 2";
            _cinema.Name = expectedName;

            // Act
            var actualName = _cinema.Name;

            // Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void City_SetGet_ShouldWorkCorrectly()
        {
            // Arrange
            var expectedCity = "City 2";
            _cinema.City = expectedCity;

            // Act
            var actualCity = _cinema.City;

            // Assert
            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test]
        public void Country_SetGet_ShouldWorkCorrectly()
        {
            // Arrange
            var expectedCountry = "Country 2";
            _cinema.Country = expectedCountry;

            // Act
            var actualCountry = _cinema.Country;

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }

        [Test]
        public void ContactPhone_SetGet_ShouldWorkCorrectly()
        {
            // Arrange
            var expectedContactPhone = "0987654321";
            _cinema.ContactPhone = expectedContactPhone;

            // Act
            var actualContactPhone = _cinema.ContactPhone;

            // Assert
            Assert.AreEqual(expectedContactPhone, actualContactPhone);
        }

        [Test]
        public void OpeningHours_SetGet_ShouldWorkCorrectly()
        {
            // Arrange
            var expectedOpeningHours = "9:00 AM - 10:00 PM";
            _cinema.OpeningHours = expectedOpeningHours;

            // Act
            var actualOpeningHours = _cinema.OpeningHours;

            // Assert
            Assert.AreEqual(expectedOpeningHours, actualOpeningHours);
        }

        // Test for Halls property initialization
        [Test]
        public void Halls_ShouldBeInitializedAsEmptyList_WhenCinemaIsCreated()
        {
            // Assert
            Assert.IsNotNull(_cinema.Halls);
            Assert.IsEmpty(_cinema.Halls);
        }

        // Test for Halls property getter and setter
        [Test]
        public void Halls_SetGet_ShouldWorkCorrectly()
        {
            // Arrange
            var hallList = new List<Hall>
            {
                new Hall(), // Assuming Hall class exists
                new Hall()  // Add more halls as needed for testing
            };

            // Act
            _cinema.Halls = hallList;
            var actualHalls = _cinema.Halls;

            // Assert
            Assert.AreEqual(hallList.Count, actualHalls.Count);
            Assert.AreEqual(hallList[0], actualHalls[0]); // Assuming Hall has overridden Equals
            Assert.AreEqual(hallList[1], actualHalls[1]);
        }
    }
}
