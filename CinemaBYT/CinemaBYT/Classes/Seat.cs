using System;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Seat
    {
        private int _seatNo;
        private bool _isVIP;
        private bool _isAvailable = true; 
        private Ticket? _ticket;

        [DisallowNull]
        public int SeatNo
        {
            get => _seatNo;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Seat number must be positive.", nameof(SeatNo));
                }
                _seatNo = value;
            }
        }

        [DisallowNull]
        public bool IsVIP
        {
            get => _isVIP;
            set => _isVIP = value;
        }

        [DisallowNull]
        public bool IsAvailable
        {
            get => _isAvailable;
            private set => _isAvailable = value; 
        }

        [AllowNull]
        public Ticket? Ticket
        {
            get => _ticket;
            private set => _ticket = value; 
        }

        public Seat(int seatNo, bool isVIP, bool isAvailable = true)
        {
            SeatNo = seatNo;
            IsVIP = isVIP;
            IsAvailable = isAvailable; 
        }

        public bool ReserveSeat(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
            }
            if (!IsAvailable)
            {
                throw new SeatReservationException("Seat is already reserved.");
            }

            Ticket = ticket;
            IsAvailable = false;
            return true;
        }

        public bool ReleaseSeat()
        {
            if (IsAvailable)
            {
                throw new SeatReservationException("Seat is already available.");
            }

            Ticket = null; 
            IsAvailable = true; 
            return true;
        }

        public override string ToString()
        {
            return $"Seat {SeatNo} - {(IsVIP ? "VIP" : "Standard")}, " +
                   $"{(IsAvailable ? "Available" : "Reserved")}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Seat other)
            {
                // Compare the key properties of the Seat class.
                return SeatNo == other.SeatNo &&
                       IsVIP == other.IsVIP &&
                       IsAvailable == other.IsAvailable;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of the essential properties.
            int hashCode = SeatNo.GetHashCode();
            hashCode = (hashCode * 397) ^ IsVIP.GetHashCode();
            hashCode = (hashCode * 397) ^ IsAvailable.GetHashCode();
    
            return hashCode;
        }

    }
}
