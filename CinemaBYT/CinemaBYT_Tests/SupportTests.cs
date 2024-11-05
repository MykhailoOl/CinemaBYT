namespace CinemaBYT_Tests;

    public class SupportTests
    {
        private Support _support;

        [SetUp]
        public void Setup()
        {
            _support = new Support(DateTime.Now.AddYears(-2), 50000, "Jane Doe", "jane.doe@example.com", new DateTime(1988, 6, 15), "12345678901", "Junior");
        }

        // Test Employee's inherited properties using Support instance

        [Test]
        public void HireDate_SetToPastDate_ShouldStoreCorrectly()
        {
            // Arrange
            var pastDate = DateTime.Now.AddYears(-2);

            // Act
            _support.HireDate = pastDate;

            // Assert
            Assert.AreEqual(pastDate, _support.HireDate);
        }

        [Test]
        public void HireDate_SetToCurrentDate_ShouldStoreCorrectly()
        {
            // Arrange
            var currentDate = DateTime.Now;

            // Act
            _support.HireDate = currentDate;

            // Assert
            Assert.AreEqual(currentDate, _support.HireDate);
        }

        [Test]
        public void HireDate_SetToFutureDate_ShouldStoreCorrectly()
        {
            // Arrange
            var futureDate = DateTime.Now.AddYears(1);

            // Act
            _support.HireDate = futureDate;

            // Assert
            Assert.AreEqual(futureDate, _support.HireDate);
        }

        // Tests for Salary property

        [Test]
        public void Salary_SetToValidPositiveValue_ShouldStoreCorrectly()
        {
            // Arrange
            var salary = 50000m;

            // Act
            _support.Salary = salary;

            // Assert
            Assert.AreEqual(salary, _support.Salary);
        }

        [Test]
        public void Salary_SetToZero_ShouldStoreCorrectly()
        {
            // Arrange
            var salary = 0m;

            // Act
            _support.Salary = salary;

            // Assert
            Assert.AreEqual(salary, _support.Salary);
        }

        [Test]
        public void Salary_SetToNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var negativeSalary = -1000m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _support.Salary = negativeSalary);
            Assert.That(exception.Message, Does.Contain("Salary cannot be negative").IgnoreCase);
        }

        // Additional tests for boundary conditions or edge cases
        

        [Test]
        public void Salary_SetToLargeDecimalValue_ShouldStoreCorrectly()
        {
            // Arrange
            var largeSalary = 1000000000m; // Example of a large salary

            // Act
            _support.Salary = largeSalary;

            // Assert
            Assert.AreEqual(largeSalary, _support.Salary);
        }
    

        // Support-specific property tests

        [Test]
        public void Level_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var level = "Senior";

            // Act
            _support.Level = level;

            // Assert
            Assert.AreEqual(level, _support.Level);
        }

        [Test]
        public void Level_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _support.Level = null);
            Assert.That(exception.Message, Does.Contain("Level cannot be null or empty").IgnoreCase);
        }

        [Test]
        public void Level_SetToWhitespace_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _support.Level = "   ");
            Assert.That(exception.Message, Does.Contain("Level cannot be null or empty").IgnoreCase);
        }
    }
