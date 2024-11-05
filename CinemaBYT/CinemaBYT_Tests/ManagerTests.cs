namespace CinemaBYT_Tests;

    public class ManagerTests
    {
        private Manager _manager;

        [SetUp]
        public void Setup()
        {
            // Initialize with valid default values for testing
            _manager = new Manager(
                hireDate: DateTime.Now.AddYears(-1),
                salary: 50000,
                position: "General Manager",
                name: "Alice Johnson",
                email: "alice.johnson@example.com",
                birthDate: new DateTime(1985, 3, 10),
                pesel: "12345678901"
            );
        }

        // Reusable Employee Tests

        [Test]
        public void HireDate_SetToPastDate_ShouldStoreCorrectly()
        {
            // Arrange
            var pastDate = DateTime.Now.AddYears(-2);

            // Act
            _manager.HireDate = pastDate;

            // Assert
            Assert.AreEqual(pastDate, _manager.HireDate);
        }

        [Test]
        public void HireDate_SetToFutureDate_ShouldStoreCorrectly()
        {
            // Arrange
            var futureDate = DateTime.Now.AddYears(1);

            // Act
            _manager.HireDate = futureDate;

            // Assert
            Assert.AreEqual(futureDate, _manager.HireDate);
        }

        [Test]
        public void Salary_SetToValidPositiveValue_ShouldStoreCorrectly()
        {
            // Arrange
            var salary = 60000m;

            // Act
            _manager.Salary = salary;

            // Assert
            Assert.AreEqual(salary, _manager.Salary);
        }

        [Test]
        public void Salary_SetToZero_ShouldStoreCorrectly()
        {
            // Arrange
            var salary = 0m;

            // Act
            _manager.Salary = salary;

            // Assert
            Assert.AreEqual(salary, _manager.Salary);
        }

        [Test]
        public void Salary_SetToNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var negativeSalary = -1000m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _manager.Salary = negativeSalary);
            Assert.That(exception.Message, Does.Contain("Salary cannot be negative").IgnoreCase);
        }

        // Tests for Manager-specific Property: Position

        [Test]
        public void Position_SetToValidString_ShouldStoreCorrectly()
        {
            // Arrange
            var position = "Operations Manager";

            // Act
            _manager.Position = position;

            // Assert
            Assert.AreEqual(position, _manager.Position);
        }

        [Test]
        public void Position_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _manager.Position = null);
            Assert.That(exception.Message, Does.Contain("Position cannot be null or empty").IgnoreCase);
        }

        [Test]
        public void Position_SetToEmptyString_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _manager.Position = "");
            Assert.That(exception.Message, Does.Contain("Position cannot be null or empty").IgnoreCase);
        }

        // Tests for Manager Constructors

        [Test]
        public void Constructor_WithAllParameters_ShouldInitializeCorrectly()
        {
            // Arrange
            var hireDate = DateTime.Now.AddYears(-2);
            var salary = 70000m;
            var position = "Regional Manager";
            var name = "Bob Smith";
            var email = "bob.smith@example.com";
            var birthDate = new DateTime(1990, 4, 15);
            var pesel = "98765432109";

            // Act
            var manager = new Manager(hireDate, salary, position, name, email, birthDate, pesel);

            // Assert
            Assert.AreEqual(hireDate, manager.HireDate);
            Assert.AreEqual(salary, manager.Salary);
            Assert.AreEqual(position, manager.Position);
            Assert.AreEqual(name, manager.Name);
            Assert.AreEqual(email, manager.Email);
            Assert.AreEqual(birthDate, manager.BirthDate);
            Assert.AreEqual(pesel, manager.PESEL);
        }

        [Test]
        public void Constructor_WithPersonAndAllParameters_ShouldInitializeCorrectly()
        {
            // Arrange
            var hireDate = DateTime.Now.AddYears(-1);
            var salary = 55000m;
            var position = "Finance Manager";
            var person = new Buyer("Linda Clark", "linda.clark@example.com", new DateTime(1988, 6, 5), "09876543210");

            // Act
            var manager = new Manager(hireDate, salary, position, person);

            // Assert
            Assert.AreEqual(hireDate, manager.HireDate);
            Assert.AreEqual(salary, manager.Salary);
            Assert.AreEqual(position, manager.Position);
            Assert.AreEqual("Linda Clark", manager.Name);
            Assert.AreEqual("linda.clark@example.com", manager.Email);
            Assert.AreEqual(new DateTime(1988, 6, 5), manager.BirthDate);
            Assert.AreEqual("09876543210", manager.PESEL);
        }

        [Test]
        public void Constructor_CopyEmployee_ShouldInitializeCorrectly()
        {
            // Arrange
            var employee = new Manager(DateTime.Now.AddYears(-3), 65000, "Assistant Manager", "Emily Davis", "emily.davis@example.com", new DateTime(1992, 8, 25), "87654321098");

            // Act
            var managerCopy = new Manager("Assistant Manager", employee);

            // Assert
            Assert.AreEqual(employee.HireDate, managerCopy.HireDate);
            Assert.AreEqual(employee.Salary, managerCopy.Salary);
            Assert.AreEqual(employee.Position, managerCopy.Position);
            Assert.AreEqual(employee.Name, managerCopy.Name);
            Assert.AreEqual(employee.Email, managerCopy.Email);
            Assert.AreEqual(employee.BirthDate, managerCopy.BirthDate);
            Assert.AreEqual(employee.PESEL, managerCopy.PESEL);
        }
    }
