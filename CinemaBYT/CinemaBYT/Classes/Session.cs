﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;
using static System.Collections.Specialized.BitVector32;

namespace CinemaBYT
{
    public class Session
    {
        private TimeSpan _duration;
        private DateTime _timeStart;
        private decimal _income;
        private Movie _movie;
        private Hall? _hall;
        private List<Ticket> _tickets=new List<Ticket>();
        private History? _history; 

        [DisallowNull]
        public TimeSpan Duration
        {
            get => _duration;
             set => _duration = value;
        }

        [DisallowNull]
        public DateTime TimeStart
        {
            get => _timeStart;
             set => _timeStart = value;
        }

        [DisallowNull]
        public decimal Income
        {
            get => _income;
            private set => _income = value;
        }

        [DisallowNull]
        public Movie Movie
        {
            get => _movie;
             set => _movie = value ?? throw new ArgumentNullException(nameof(Movie), "Movie cannot be null.");
        }
        public void deleteSession() {
            _hall = null;
            foreach (var ticket in _tickets.ToList())
            {
                ticket.DeleteSession();
            }
            _tickets.Clear();
            _history.deleteSessionInHistory(this);
            _history = null;
            if (_movie != null)
            {
                try
                {
                    _movie.deleteSession(this);
                }
                catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                _movie = null;
            }
        }
        public static void deleteSessionGlobally(Session s)
        {
            s.deleteSession();
            s = null;
        }
        public void AddTicket(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException();
            if (!_tickets.Contains(ticket))
            {
                ticket.updateSession(this);
                _tickets.Add(ticket);
            }
        }
        public void DeleteTicket(Ticket t)
        {
            if(t== null) throw new ArgumentNullException();
            if(_tickets.Contains(t))
                _tickets.Remove(t);
            Ticket.deleteTicket(t);
        }
        public void UpdateMovie(Movie m)
        {
            if(m == null) throw new ArgumentNullException();
            if(m!=_movie) _movie.updateItself(m);
        }
        public void ReplaceMovie(Movie m)
        {
            if(null == m) throw new ArgumentNullException();
            if(_movie!=m)
            {
                Movie.deleteMovie(_movie);
                _movie.updateItself(m);
            }
        }
        public void UpdateHall(Hall hall)
        {
            if (hall == null)
                throw new ArgumentNullException();
            if (hall != _hall)
            {
                _hall.deleteSession(this);
                _hall = hall;
                _hall.addSession(this);
            }
        }

            [DisallowNull]
        public Hall Hall
        {
            get => _hall;
             set => _hall = value ?? throw new ArgumentNullException(nameof(Hall), "Hall cannot be null.");
        }

        [AllowNull]
        public List<Ticket> Tickets
        {
            get => _tickets;
             set => _tickets = value;
        }

        [AllowNull]
        public History? History
        {
            get => _history;
             set => _history = value;
        }

        public Session()
        {
        }

        public Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall, List<Ticket> tickets, History? history = null)
        {
            Duration = duration;
            TimeStart = timeStart;
            Income = income;
            Movie = movie;
            Hall = hall;
            History = history;
            Tickets = tickets;
        }

        public bool CheckAvailability()
        {
            if (Hall == null)
            {
                throw new SessionException("The session's hall is not initialized.");
            }

            return Hall.NumberOfSeats > Tickets.Count; 
        }

        public void AddIncome(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Income amount cannot be negative.", nameof(amount));
            }
            Income += amount;
        }

        //public override string ToString()
        //{
        //    return $"{Movie.Name} session in Hall {Hall.HallNumber} starts at {TimeStart:HH:mm} with duration of {Duration}";
        //}
        public override bool Equals(object obj)
        {
            if (obj is Session other)
            {
                // Compare the key properties of the Session class, ensuring null safety for reference types.
                return Movie?.Equals(other.Movie) == true &&
                       Hall?.Equals(other.Hall) == true &&
                       TimeStart == other.TimeStart;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Ensure hash code computation takes into account null-safe operations.
            int hashCode = Movie?.GetHashCode() ?? 0;  // Use 0 if Movie is null.
            hashCode = (hashCode * 397) ^ (Hall?.GetHashCode() ?? 0);  // Use 0 if Hall is null.
            hashCode = (hashCode * 397) ^ TimeStart.GetHashCode();  // TimeStart is a value type, so no need for null check.
    
            return hashCode;
        }


    }
}
