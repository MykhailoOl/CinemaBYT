using System;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Ticket
    {
        public int SeatNumber { get; private set; }
        public decimal Price { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public TicketType Type { get; private set; }
        public Session Session { get; private set; }
        public Seat Seat { get; private set; }
        public Person Person { get; private set; }

        public Ticket(int seatNumber, decimal price, DateTime purchaseDate, TicketType type, Session session, Seat seat, Person person)
        {
            if (seatNumber <= 0)
            {
                throw new ArgumentException("Seat number must be positive.", nameof(seatNumber));
            }

            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            }

            SeatNumber = seatNumber;
            Price = price;
            PurchaseDate = purchaseDate;
            Type = type;
            Session = session ?? throw new ArgumentNullException(nameof(session), "Session cannot be null.");
            Seat = seat ?? throw new ArgumentNullException(nameof(seat), "Seat cannot be null.");
            Person = person ?? throw new ArgumentNullException(nameof(person), "Person cannot be null.");
        }

        public bool BuyTicket()
        {
            if (Seat == null)
            {
                throw new TicketException("Seat is not initialized.");
            }

            if (!Seat.IsAvailable)
            {
                Console.WriteLine("Seat is already reserved.");
                return false;
            }

            if (Seat.ReserveSeat(this))
            {
                Console.WriteLine("Ticket purchased successfully!");
                return true;
            }

            return false;
        }

        public bool RefundTicket()
        {
            if (Seat == null)
            {
                throw new TicketException("Seat is not initialized.");
            }

            if (CanRefund())
            {
                if (Seat.ReleaseSeat())
                {
                    Console.WriteLine("Ticket refunded successfully.");
                    return true;
                }

                Console.WriteLine("Failed to release the seat.");
                return false;
            }

            Console.WriteLine("Ticket cannot be refunded; refund period has expired.");
            return false;
        }

        private bool CanRefund()
        {
            TimeSpan timeDifference = DateTime.Now - PurchaseDate;
            return timeDifference.TotalHours < 24;
        }

        public override string ToString()
        {
            return $"Ticket for Seat {SeatNumber} - {Type} at {Session.TimeStart:HH:mm} for {Price:C}";
        }
    }

    public enum TicketType
    {
        Adult,
        Senior,
        Child
    }
}
