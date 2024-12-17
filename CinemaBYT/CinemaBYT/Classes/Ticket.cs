using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Ticket
    {
        private int _ticketId;
        private decimal _price;
        private DateTime _purchaseDate;
        private TicketType _type;
        private Session _session;
        private Seat _seat;
        private Person _person;
        private List<Payment> _payments = new List<Payment>();

        [DisallowNull]
        public int TicketId
        {
            get => _ticketId;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Ticket id must be positive.", nameof(TicketId));
                }
                _ticketId = value;
            }
        }
        public void addPayment(Payment payment) { 
        _payments.Add(payment);
        }
        public void setPerson(Person person) { 
        _person = person;
        }

        [DisallowNull]
        public decimal Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative.", nameof(Price));
                }
                _price = value;
            }
        }

        [DisallowNull]
        public DateTime PurchaseDate
        {
            get => _purchaseDate;
            private set => _purchaseDate = value;
        }

        [DisallowNull]
        public TicketType Type
        {
            get => _type;
            private set => _type = value;
        }

        [DisallowNull]
        public Session Session
        {
            get => _session;
            private set => _session = value ?? throw new ArgumentNullException(nameof(Session), "Session cannot be null.");
        }

        [DisallowNull]
        public Seat Seat
        {
            get => _seat;
            private set => _seat = value ?? throw new ArgumentNullException(nameof(Seat), "Seat cannot be null.");
        }

        [DisallowNull]
        public Person Person
        {
            get => _person;
            private set => _person = value ?? throw new ArgumentNullException(nameof(Person), "Person cannot be null.");
        }
        [AllowNull]
        public List<Payment> Payments
        {
            get => _payments;
            set => _payments = value ?? new List<Payment>();
        }

        public object Current => throw new NotImplementedException();

        public Ticket(int seatNumber, decimal price, DateTime purchaseDate, TicketType type, Session session, Seat seat, Person person)
        {
            TicketId = seatNumber; 
            Price = price;         
            PurchaseDate = purchaseDate;
            Type = type;
            Session = session;       
            Seat = seat;             
            Person = person;        
        }

        public Ticket()
        {
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

        //public override string ToString()
        //{
        //    return $"Ticket for Seat {SeatNumber} - {Type} at {Session.TimeStart:HH:mm} for {Price:C}";
        //}
        public override bool Equals(object obj)
        {
            if (obj is Ticket other)
            {
                // Compare properties that define equality.
                return TicketId == other.TicketId &&
                       Type == other.Type &&
                       Session.Equals(other.Session) && // Ensure the session objects are equal
                       Person.Equals(other.Person);     // Ensure the person objects are equal
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of the relevant properties.
            int hashCode = TicketId.GetHashCode();
            hashCode = (hashCode * 397) ^ Type.GetHashCode();
            hashCode = (hashCode * 397) ^ Session.GetHashCode(); // Ensure the session hash code is used
            hashCode = (hashCode * 397) ^ Person.GetHashCode();  // Ensure the person hash code is used
            return hashCode;
        }
        public void DeleteSession()
        {
            _session = null;
        }
        public void deleteSeat()
        {
            _seat = null;
        }
        public static void deleteTicket(Ticket t)
        {
            if (t != null)
            {
                t._person.deleteTicket(t);
                t.DeleteSession();
                t.deleteSeat();
                t = null;
            }
        }

        internal void updateSession(Session session)
        {
            if (session == null)
                throw new ArgumentNullException();
            if(_session!=session)
            {
                _session = session;
                _session.AddTicket(this);
            }
        }

      
    }

    public enum TicketType
    {
        Adult,
        Senior,
        Child
    }

}
