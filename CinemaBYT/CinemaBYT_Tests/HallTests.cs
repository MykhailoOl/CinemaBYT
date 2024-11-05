using System.ComponentModel.DataAnnotations;
using CinemaBYT;
using CinemaBYT.Exceptions;

namespace CinemaBYT_Tests;


public class HallTests
    {
        private List<Seat> _validSeats;
        private Cinema _cinema;

        [SetUp]
        public void Setup()
        {
            // Creating a valid list of seats
            _validSeats = new List<Seat>();
            for (int i = 1; i <= 30; i++)
            {
                _validSeats.Add(new Seat(seatNo: i, isVIP: i % 2 == 0)); // Every second seat is VIP
            }

            _cinema = new Cinema("Cinema Name", "City", "Country", "1234567890", "9:00 AM - 10:00 PM");
        }

        // Constructor Tests

        [Test]
        public void Hall_Constructor_ValidParameters_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Assert
            Assert.AreEqual(1, hall.HallNumber);
            Assert.AreEqual(30, hall.NumberOfSeats);
            Assert.AreEqual(_validSeats, hall.Seats);
            Assert.AreEqual(_cinema, hall.Cinema);
        }

        [Test]
        public void Hall_Constructor_InvalidNumberOfSeats_ShouldThrowValidationException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ValidationException>(() => new Hall(hallNumber: 1, numberOfSeats: 15, seats: _validSeats));
            Assert.That(exception.Message, Does.Contain("Number of seats must be between 20 and 100"));
        }

        [Test]
        public void Hall_Constructor_NullSeats_ShouldThrowArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Hall(hallNumber: 1, numberOfSeats: 30, seats: null));
            Assert.That(exception.Message, Does.Contain("Seats list cannot be null."));
        }

        [Test]
        public void Hall_Constructor_InvalidSeatCount_ShouldThrowValidationException()
        {
            // Arrange
            var invalidSeats = new List<Seat> { new Seat(1, false), new Seat(2, false) }; // Only 2 seats

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => new Hall(hallNumber: 1, numberOfSeats: 30, seats: invalidSeats));
            Assert.That(exception.Message, Does.Contain("The number of seats provided (2) does not match the specified capacity (30)."));
        }

        // Getter/Setter Tests

        [Test]
        public void Hall_SetCinema_NullCinema_ShouldThrowArgumentNullException()
        {
            // Arrange
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => hall.SetCinema(null));
            Assert.That(exception.Message, Does.Contain("Cinema cannot be null."));
        }

        [Test]
        public void Hall_GetCinema_ShouldReturnCinema_WhenSet()
        {
            // Arrange
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act
            var result = hall.GetCinema();

            // Assert
            Assert.AreEqual(_cinema, result);
        }

        [Test]
        public void Hall_NumberOfSeats_SetInvalidValue_ShouldThrowValidationException()
        {
            // Arrange
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => hall.NumberOfSeats = 15);
            Assert.That(exception.Message, Does.Contain("Number of seats must be between 20 and 100"));
        }

        [Test]
        public void Hall_Seats_SetNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => hall.Seats = null);
            Assert.That(exception.Message, Does.Contain("Seats list cannot be null."));
        }

        // Method Tests

        [Test]
        public void Hall_HasAvailableSeats_AllSeatsAvailable_ShouldReturnTrue()
        {
            // Arrange
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act
            var result = hall.HasAvailableSeats();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Hall_HasAvailableSeats_NoAvailableSeats_ShouldReturnFalse()
        {
            // Arrange
            foreach (var seat in _validSeats)
            {
                seat.ReserveSeat(new Ticket(/* assume valid ticket properties */));
            }
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act
            var result = hall.HasAvailableSeats();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Hall_ValidateSeats_ValidSeatCount_ShouldNotThrowException()
        {
            // Arrange
            var hall = new Hall(hallNumber: 1, numberOfSeats: 30, seats: _validSeats, cinema: _cinema);

            // Act & Assert
            Assert.DoesNotThrow(() => hall.ValidateSeats());
        }

        
    }
