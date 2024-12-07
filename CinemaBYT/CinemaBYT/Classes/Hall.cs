using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Hall
    {
        private int _numberOfSeats;
        private List<Seat> _seats;
        private Cinema? _cinema;
        private int _hallNumber;
        private List<Session> _sessions;

        [Range(20, 100, ErrorMessage = "The number of seats in the hall must be between 20 and 100.")]
        public int NumberOfSeats
        {
            get => _numberOfSeats;
            set
            {
                if (value < 20 || value > 100)
                {
                    throw new ValidationException("Number of seats must be between 20 and 100. Current count: {value}");
                }
                _numberOfSeats = value;
            }
        }

        [DisallowNull]
        public List<Seat> Seats
        {
            get => _seats;
             set => _seats = value ?? throw new ArgumentNullException(nameof(Seats), "Seats list cannot be null.");
        }

        public List<Session> Sessions { get; private set; } = new List<Session>();

        public Cinema? Cinema
        {
            get => _cinema;
            private set => _cinema = value;
        }

        public void addCinema(Cinema cinema)
        {
            if (_cinema != cinema)
            {
                if (_cinema!=null) 
                    _cinema.deleteHall(this);
                _cinema = cinema;
                if (!cinema.halls.Contains(this))
                {
                    cinema.addHall(this);
                }
            }
        }
        public void deleteCinema() {
            Cinema = null;
            foreach (var seat in _seats.ToList())
            {
                seat.deleteHall();
                if(seat.Ticket!=null)
                    seat.deleteTicket();
                
            }
            _seats.Clear();
            foreach (var session in Sessions.ToList())
            {
                session.deleteSession();
            }
            Sessions.Clear();
        }
        public int HallNumber
        {
            get => _hallNumber;
            private set => _hallNumber = value;
        }

        public Hall(int hallNumber, int numberOfSeats, List<Seat> seats, Cinema? cinema = null)
        {
            HallNumber = hallNumber; 
            NumberOfSeats = numberOfSeats;
            Seats = seats; 
            Cinema = cinema;
            ValidateSeats();
        }

        public Hall()
        {
        }

        public bool HasAvailableSeats()
        {
            if (Seats == null || Seats.Count == 0)
            {
                throw new HallException("The hall has no seats initialized or the seats list is empty.");
            }

            foreach (var seat in Seats)
            {
                if (seat.IsAvailable)
                {
                    return true;
                }
            }

            return false;
        }

        public void ValidateSeats()
        {
            if (Seats.Count != NumberOfSeats)
            {
                throw new ValidationException($"The number of seats provided ({Seats.Count}) does not match the specified capacity ({NumberOfSeats}).");
            }
        }

        public void SetCinema(Cinema cinema)
        {
            Cinema = cinema ?? throw new ArgumentNullException(nameof(cinema), "Cinema cannot be null.");
        }

        public Cinema? GetCinema()
        {
            return Cinema;
        }
        public override bool Equals(object obj)
        {
            if (obj is Hall otherHall)
            {
                // Compare basic properties
                return HallNumber == otherHall.HallNumber &&
                       NumberOfSeats == otherHall.NumberOfSeats &&
                       Seats.SequenceEqual(otherHall.Seats) &&
                       EqualityComparer<Cinema?>.Default.Equals(Cinema, otherHall.Cinema);
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of relevant properties
            return HashCode.Combine(HallNumber, NumberOfSeats, Seats, Cinema);
        }

        public void deleteSession(Session session)
        {
            if (session == null) throw new ArgumentNullException();
            if(_sessions.Remove(session))
                Session.deleteSessionGlobally(session);
            
        }

        public void addSession(Session session)
        {
            if(session == null) throw new ArgumentNullException();
            if (!_sessions.Contains(session))
            {
                _sessions.Add(session);
                session.UpdateHall(this);
            }
        }
    }
}
