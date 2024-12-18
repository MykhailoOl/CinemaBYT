﻿namespace CinemaBYT_Tests;

public class OwnsLoyaltyCardTests
    {
        private string _validName;
        private string _validEmail;
        private DateTime _validBirthDate;
        private string _validPESEL;
        private DateTime _validStartDate;
        private DateTime _validExpireDate;

        [SetUp]
        public void Setup()
        {
            _validName = "John Doe";
            _validEmail = "john.doe@example.com";
            _validBirthDate = new DateTime(1990, 1, 1);
            _validPESEL = "12345678901"; // Example PESEL
            _validStartDate = DateTime.Now.AddMonths(-1); // Start date 1 month ago
            _validExpireDate = DateTime.Now.AddMonths(1); // Expiry date 1 month from now
        }

        // Test inherited properties from Person class (Name, Email, BirthDate, PESEL)

        [Test]
        public void Constructor_ValidParameters_ShouldInitializePersonProperties()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate);

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
            var exception = Assert.Throws<ArgumentNullException>(() => new OwnsLoyaltyCard(null, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate));
            Assert.AreEqual("Name cannot be null or empty. (Parameter 'Name')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidEmail_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OwnsLoyaltyCard(_validName, null, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate));
            Assert.AreEqual("Email cannot be null or empty. (Parameter 'Email')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidBirthDate_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, DateTime.Now.AddDays(1), _validPESEL, _validStartDate, _validExpireDate));
            Assert.AreEqual("Birth date cannot be in the future. (Parameter 'BirthDate')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidPESEL_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, "12345", _validStartDate, _validExpireDate));
            Assert.AreEqual("PESEL must be a valid 11-character identifier. (Parameter 'PESEL')", exception.Message);
        }

        // Test for StartDate

        [Test]
        public void StartDate_SetFutureDate_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var futureStartDate = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, futureStartDate, _validExpireDate));
            Assert.AreEqual("Start date cannot be in the future. (Parameter 'StartDate')", exception.Message);
        }

        [Test]
        public void StartDate_SetValidDate_ShouldSetValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate);

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
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, expireDateBeforeStartDate));
            Assert.AreEqual("Expire date must be after the start date. (Parameter 'ExpireDate')", exception.Message);
        }

        [Test]
        public void ExpireDate_SetValidDate_ShouldSetValue()
        {
            // Act
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate);

            // Assert
            Assert.AreEqual(_validExpireDate, loyaltyCardOwner.ExpireDate);
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
        }
        

        // Test Null Properties for Person class
        [Test]
        public void Constructor_NullEmail_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OwnsLoyaltyCard(_validName, null, _validBirthDate, _validPESEL, _validStartDate, _validExpireDate));
            Assert.AreEqual("Email cannot be null or empty. (Parameter 'Email')", exception.Message);
        }

        [Test]
        public void Constructor_EmptyPESEL_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, "", _validStartDate, _validExpireDate));
            Assert.AreEqual("PESEL must be a valid 11-character identifier. (Parameter 'PESEL')", exception.Message);
        }

        [Test]
        public void Constructor_InvalidPESEL_Length_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, "123456789012", _validStartDate, _validExpireDate));
            Assert.AreEqual("PESEL must be a valid 11-character identifier. (Parameter 'PESEL')", exception.Message);
        }
        
        [Test]
        public void Discount_WithMoreThan30DaysRemaining_ShouldReturnZero()
        {
            // Arrange
            var expireDate = DateTime.Now.AddDays(31);
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, expireDate);

            // Act
            var discount = loyaltyCardOwner.Discount;

            // Assert
            Assert.AreEqual(0m, discount);
        }

        [Test]
        public void Discount_WithExactly1DayRemaining_ShouldReturn20Percent()
        {
            // Arrange
            var expireDate = DateTime.Now.AddDays(1);
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, expireDate);

            // Act
            var discount = loyaltyCardOwner.Discount;

            // Assert
            Assert.AreEqual(20m, discount); // 20% discount
        }

        [Test]
        public void Discount_WithLessThan1DayRemaining_ShouldReturn20Percent()
        {
            // Arrange
            var expireDate = DateTime.Now.AddHours(23);
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, expireDate);

            // Act
            var discount = loyaltyCardOwner.Discount;

            // Assert
            Assert.AreEqual(20m, discount); // 20% discount
        }

        [Test]
        public void Discount_With15DaysRemaining_ShouldReturnProportionalDiscount()
        {
            // Arrange
            var expireDate = DateTime.Now.AddDays(15); // Halfway to expiry
            var loyaltyCardOwner = new OwnsLoyaltyCard(_validName, _validEmail, _validBirthDate, _validPESEL, _validStartDate, expireDate);

            // Act
            var discount = loyaltyCardOwner.Discount;

            // Assert
            // 15 days remaining, so proportional discount = (1 - (15 / 30)) * 0.2 = 10% = 10m
            Assert.AreEqual(10m, Math.Round(discount, 2));
        }
        


    }