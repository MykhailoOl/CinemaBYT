using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Session
    {
        private TimeSpan _duration;
        private DateTime _timeStart;
        private decimal _income;
        private Movie _movie;
        private Hall _hall;
        private List<Ticket> _tickets;
        private History? _history; 

        [DisallowNull]
        public TimeSpan Duration
        {
            get => _duration;
            private set => _duration = value;
        }

        [DisallowNull]
        public DateTime TimeStart
        {
            get => _timeStart;
            private set => _timeStart = value;
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
            private set => _movie = value ?? throw new ArgumentNullException(nameof(Movie), "Movie cannot be null.");
        }

        [DisallowNull]
        public Hall Hall
        {
            get => _hall;
            private set => _hall = value ?? throw new ArgumentNullException(nameof(Hall), "Hall cannot be null.");
        }

        [DisallowNull]
        public List<Ticket> Tickets
        {
            get => _tickets;
            private set => _tickets = value ?? throw new ArgumentNullException(nameof(Tickets), "Tickets list cannot be null.");
        }

        [AllowNull]
        public History? History
        {
            get => _history;
            private set => _history = value;
        }

        public Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall, List<Ticket> tickets, History? history = null)
        {
            Duration = duration;
            TimeStart = timeStart;
            Income = income;
            Movie = movie;
            Hall = hall;
            Tickets = tickets;

            if (tickets.Count == 0)
            {
                throw new SessionException("The session must have at least one ticket.");
            }

            History = history; 
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

        public override string ToString()
        {
            return $"{Movie.Name} session in Hall {Hall.HallNumber} starts at {TimeStart:HH:mm} with duration of {Duration}";
        }
    }
}
