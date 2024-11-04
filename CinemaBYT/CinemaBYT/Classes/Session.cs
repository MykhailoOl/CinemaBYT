using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Session
    {
        public TimeSpan Duration { get; private set; }
        public DateTime TimeStart { get; private set; }
        public decimal Income { get; private set; }
        public Movie Movie { get; private set; }
        public Hall Hall { get; private set; }
        public List<Ticket> Tickets { get; private set; }
        public History? History { get; private set; }

        public Session(TimeSpan duration, DateTime timeStart, decimal income, Movie movie, Hall hall, List<Ticket> tickets, History? history = null)
        {
            Duration = duration;
            TimeStart = timeStart;
            Income = income;
            Movie = movie ?? throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");
            Hall = hall ?? throw new ArgumentNullException(nameof(hall), "Hall cannot be null.");
            Tickets = tickets ?? throw new ArgumentNullException(nameof(tickets), "Tickets list cannot be null.");

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
