using CinemaBYT.Exceptions;

namespace CinemaBYT_Tests;

public class PaymentTests
    {
        private PaymentType _validPaymentType;
        private DateTime _validPaymentDate;
        private int _validMaxTickets;

        [SetUp]
        public void Setup()
        {
            _validPaymentType = PaymentType.CreditCard;
            _validPaymentDate = DateTime.Now;
            _validMaxTickets = 5;
        }

        // Test 1: Constructor with valid parameters
        [Test]
        public void Constructor_ValidParameters_ShouldInitializePayment()
        {
            // Act
            var payment = new Payment(_validPaymentType, _validPaymentDate, _validMaxTickets);

            // Assert
            Assert.AreEqual(_validPaymentType, payment.Type);
            Assert.AreEqual(_validPaymentDate, payment.PaymentDate);
            Assert.AreEqual(_validMaxTickets, payment.MaxTicketPerPayment);
        }

        // Test 2: Constructor with default payment date
        [Test]
        public void Constructor_DefaultPaymentDate_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Payment(_validPaymentType, default, _validMaxTickets));
            Assert.AreEqual("Payment date cannot be default. (Parameter 'paymentDate')", exception.Message);
        }

        // Test 3: Constructor with valid max tickets
        [Test]
        public void Constructor_ValidMaxTickets_ShouldInitializePayment()
        {
            // Act
            var payment = new Payment(_validPaymentType, _validPaymentDate, 10);

            // Assert
            Assert.AreEqual(10, payment.MaxTicketPerPayment);
        }

        // Test 4: Constructor with negative max tickets
        [Test]
        public void Constructor_NegativeMaxTickets_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Payment(_validPaymentType, _validPaymentDate, -1));
            Assert.AreEqual("Max tickets per payment must be positive. (Parameter 'MaxTicketPerPayment')", exception.Message);
        }

        // Test 5: PaymentDate setter with default value
        [Test]
        public void PaymentDate_SetDefaultValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var payment = new Payment(_validPaymentType, _validPaymentDate);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => payment.PaymentDate = default);
            Assert.AreEqual("Payment date cannot be null or default. (Parameter 'PaymentDate')", exception.Message);
        }

        // Test 6: MaxTicketPerPayment setter with zero
        [Test]
        public void MaxTicketPerPayment_SetZero_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var payment = new Payment(_validPaymentType, _validPaymentDate);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => payment.MaxTicketPerPayment = 0);
            Assert.AreEqual("Max tickets per payment must be positive. (Parameter 'MaxTicketPerPayment')", exception.Message);
        }

        // Test 7: LoyaltyDiscount with positive price and loyalty card
        [Test]
        public void LoyaltyDiscount_PositivePrice_WithLoyaltyCard_ShouldApplyDiscount()
        {
            // Arrange
            double price = 100.0;

            // Act
            double discountedPrice = Payment.LoyaltyDiscount(price, true);

            // Assert
            Assert.AreEqual(90.0, discountedPrice);
        }

        // Test 8: LoyaltyDiscount with positive price and no loyalty card
        [Test]
        public void LoyaltyDiscount_PositivePrice_NoLoyaltyCard_ShouldReturnFullPrice()
        {
            // Arrange
            double price = 100.0;

            // Act
            double discountedPrice = Payment.LoyaltyDiscount(price, false);

            // Assert
            Assert.AreEqual(100.0, discountedPrice);
        }

        // Test 9: LoyaltyDiscount with zero price
        [Test]
        public void LoyaltyDiscount_ZeroPrice_ShouldThrowPaymentException()
        {
            // Act & Assert
            var exception = Assert.Throws<PaymentException>(() => Payment.LoyaltyDiscount(0, true));
            Assert.AreEqual("Price must be a positive value.", exception.Message);
        }

        // Test 10: LoyaltyDiscount with negative price
        [Test]
        public void LoyaltyDiscount_NegativePrice_ShouldThrowPaymentException()
        {
            // Act & Assert
            var exception = Assert.Throws<PaymentException>(() => Payment.LoyaltyDiscount(-50, true));
            Assert.AreEqual("Price must be a positive value.", exception.Message);
        }
    }