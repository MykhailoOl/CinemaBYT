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

        [Required]
        public int HallNumber { get; set; }

        [Range(20, 100, ErrorMessage = "The number of seats in the hall must be between 20 and 100.")]
        public int NumberOfSeats
        {
            get => _numberOfSeats;
            set
            {
                if (value < 20 || value > 100)
                {
                    throw new ValidationException($"Number of seats must be between 20 and 100. Current count: {value}");
                }
                _numberOfSeats = value;
            }
        }

        public List<Seat> Seats { get; set; }

        public List<Session> Sessions { get; set; } = new List<Session>();

        private Cinema? _cinema;

        public Cinema? Cinema
        {
            get => _cinema;
            private set => _cinema = value;
        }

        public Hall(int hallNumber, int numberOfSeats, List<Seat> seats, Cinema? cinema = null)
        {
            HallNumber = hallNumber;
            NumberOfSeats = numberOfSeats;
            Seats = seats ?? throw new ArgumentNullException(nameof(seats), "Seats list cannot be null.");
            Cinema = cinema;
            ValidateSeats();
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
    }
}
