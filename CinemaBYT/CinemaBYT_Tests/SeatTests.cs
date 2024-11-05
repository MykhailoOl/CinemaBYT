using CinemaBYT;
using CinemaBYT.Exceptions;

namespace CinemaBYT_Tests;

public class SeatTests
    {
        private const int ValidSeatNumber = 1;
        private const bool IsVIP = true;
        private const bool IsAvailable = true;

        // Test for Constructor
        [Test]
        public void Constructor_ValidParameters_ShouldCreateSeat()
        {
            // Act
            var seat = new Seat(ValidSeatNumber, IsVIP, IsAvailable);

            // Assert
            Assert.AreEqual(ValidSeatNumber, seat.SeatNo);
            Assert.AreEqual(IsVIP, seat.IsVIP);
            Assert.AreEqual(IsAvailable, seat.IsAvailable);
        }

        // Test for SeatNo property
        [Test]
        public void SeatNo_SetNegativeValue_ShouldThrowArgumentException()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP, IsAvailable);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => seat.SeatNo = -1);
            Assert.AreEqual("Seat number must be positive. (Parameter 'SeatNo')", exception.Message);
        }

        [Test]
        public void SeatNo_SetZeroValue_ShouldThrowArgumentException()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP, IsAvailable);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => seat.SeatNo = 0);
            Assert.AreEqual("Seat number must be positive. (Parameter 'SeatNo')", exception.Message);
        }

        [Test]
        public void IsVIP_SetValue_ShouldChangeValue()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);

            // Act
            seat.IsVIP = false;

            // Assert
            Assert.IsFalse(seat.IsVIP);
        }

        // Test for ReserveSeat method
        [Test]
        public void ReserveSeat_ValidTicket_ShouldReserveSeat()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);
            var ticket = new Ticket(); // Assuming Ticket class is defined elsewhere

            // Act
            bool result = seat.ReserveSeat(ticket);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(seat.IsAvailable);
            Assert.IsNotNull(seat.Ticket);
        }

        [Test]
        public void ReserveSeat_AlreadyReserved_ShouldThrowSeatReservationException()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);
            var ticket1 = new Ticket(); // Assuming Ticket class is defined elsewhere
            seat.ReserveSeat(ticket1); // Reserve the seat first

            // Act & Assert
            var exception = Assert.Throws<SeatReservationException>(() => seat.ReserveSeat(new Ticket()));
            Assert.AreEqual("Seat is already reserved.", exception.Message);
        }

        [Test]
        public void ReserveSeat_NullTicket_ShouldThrowArgumentNullException()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => seat.ReserveSeat(null));
            Assert.AreEqual("Ticket cannot be null. (Parameter 'ticket')", exception.Message);
        }

        // Test for ReleaseSeat method
        [Test]
        public void ReleaseSeat_ReservedSeat_ShouldMakeItAvailable()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);
            var ticket = new Ticket(); // Assuming Ticket class is defined elsewhere
            seat.ReserveSeat(ticket); // Reserve the seat first

            // Act
            bool result = seat.ReleaseSeat();

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(seat.IsAvailable);
            Assert.IsNull(seat.Ticket);
        }

        [Test]
        public void ReleaseSeat_AlreadyAvailable_ShouldThrowSeatReservationException()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);

            // Act & Assert
            var exception = Assert.Throws<SeatReservationException>(() => seat.ReleaseSeat());
            Assert.AreEqual("Seat is already available.", exception.Message);
        }

        // Test for ToString method
        [Test]
        public void ToString_ShouldReturnCorrectString()
        {
            // Arrange
            var seat = new Seat(ValidSeatNumber, IsVIP);

            // Act
            string result = seat.ToString();

            // Assert
            Assert.AreEqual("Seat 1 - VIP, Available", result);
        }
    }