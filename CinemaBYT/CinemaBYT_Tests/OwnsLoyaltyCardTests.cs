namespace CinemaBYT_Tests;

public class OwnsLoyaltyCardTests
    {
        private string _validName;
        private string _validEmail;
        private DateTime _validBirthDate;
        private string _validPESEL;
        private DateTime _validStartDate;
        private DateTime _validExpireDate;
        private decimal _validDiscount;

        [SetUp]
        public void Setup()
        {
            _validName = "John Doe";
            _validEmail = "john.doe@example.com";
            _validBirthDate = new DateTime(1990, 1, 1);
            _validPESEL = "12345678901"; // Example PESEL
            _validStartDate = DateTime.Now.AddMonths(-1); // Start date 1 month ago
            _validExpireDate = DateTime.Now.AddMonths(1); // Expiry date 1 month from now
            _validDiscount = 20; // 20% discount
        }

        // Test inherited properties from Person class (Name, Email, BirthDate, PESEL)

        [Test]
        public void Constructor_ValidParameters_ShouldInitializePersonProperties()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount);

            // Assert
            Assert.AreEqual(_validName, loyaltyCardOwner.Name);
            Assert.AreEqual(_validEmail, loyaltyCardOwner.Email);
            Assert.AreEqual(_validBirthDate, loyaltyCardOwner.BirthDate);
            Assert.AreEqual(_validPESEL, loyaltyCardOwner.PESEL);
        }

        [Test]
        public void Constructor_InvalidName_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OwnsLoyaltyCard(null, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("Name cannot be null or empty. (Parameter 'Name')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidEmail_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OwnsLoyaltyCard(_validName, null, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("Email cannot be null or empty. (Parameter 'Email')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidBirthDate_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, DateTime.Now.AddDays(1), _validPESEL, _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("Birth date cannot be in the future. (Parameter 'BirthDate')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidPESEL_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, "12345", _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("PESEL must be a valid 11-character identifier. (Parameter 'PESEL')", exception.Message);
        }

        // Test for StartDate

        [Test]
        public void StartDate_SetFutureDate_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var futureStartDate = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, futureStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("Start date cannot be in the future. (Parameter 'StartDate')", exception.Message);
        }

        [Test]
        public void StartDate_SetValidDate_ShouldSetValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount);

            // Assert
            Assert.AreEqual(_validStartDate, loyaltyCardOwner.StartDate);
        }

        // Test for ExpireDate

        [Test]
        public void ExpireDate_SetBeforeStartDate_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var expireDateBeforeStartDate = _validStartDate.AddDays(-1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, expireDateBeforeStartDate, _validDiscount));
            Assert.AreEqual("Expire date must be after the start date. (Parameter 'ExpireDate')", exception.Message);
        }

        [Test]
        public void ExpireDate_SetValidDate_ShouldSetValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount);

            // Assert
            Assert.AreEqual(_validExpireDate, loyaltyCardOwner.ExpireDate);
        }

        // Test for Discount

        [Test]
        public void Discount_SetNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, -5));
            Assert.AreEqual("Discount must be between 0 and 100. (Parameter 'Discount')", exception.Message);
        }

        [Test]
        public void Discount_SetGreaterThan100_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, 110));
            Assert.AreEqual("Discount must be between 0 and 100. (Parameter 'Discount')", exception.Message);
        }

        [Test]
        public void Discount_SetValidValue_ShouldSetValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount);

            // Assert
            Assert.AreEqual(_validDiscount, loyaltyCardOwner.Discount);
        }

        [Test]
        public void Discount_Getter_ShouldReturnCorrectValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount);

            // Assert
            Assert.AreEqual(_validDiscount, loyaltyCardOwner.Discount); // Discount should be returned as a percentage (multiplied by 100)
        }

        

        // Test Default Constructor
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard();

            // Assert
            Assert.IsNull(loyaltyCardOwner.Name);
            Assert.IsNull(loyaltyCardOwner.Email);
            Assert.AreEqual(default(DateTime), loyaltyCardOwner.BirthDate);
            Assert.IsNull(loyaltyCardOwner.PESEL);
            Assert.AreEqual(default(DateTime), loyaltyCardOwner.StartDate);
            Assert.AreEqual(default(DateTime), loyaltyCardOwner.ExpireDate);
            Assert.AreEqual(0, loyaltyCardOwner.Discount);
        }

        // Test Edge Cases for Discount
        [Test]
        public void Discount_SetZero_ShouldSetCorrectValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, 0);

            // Assert
            Assert.AreEqual(0, loyaltyCardOwner.Discount);
        }

        [Test]
        public void Discount_SetOneHundred_ShouldSetCorrectValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, 100);

            // Assert
            Assert.AreEqual(100, loyaltyCardOwner.Discount);
        }

        // Test Null Properties for Person class
        [Test]
        public void Constructor_NullEmail_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OwnsLoyaltyCard(_validName, null, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("Email cannot be null or empty. (Parameter 'Email')", exception.Message);
        }

        [Test]
        public void Constructor_EmptyPESEL_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, "", _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("PESEL must be a valid 11-character identifier. (Parameter 'PESEL')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidPESEL_Length_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, "123456789012", _validStartDate, _validExpireDate, _validDiscount));
            Assert.AreEqual("PESEL must be a valid 11-character identifier. (Parameter 'PESEL')", exception.Message);
        }

    }