using System.ComponentModel.DataAnnotations;
using CinemaBYT.Exceptions;

public class Hall
{
    public int HallNumber { get; set; }
    public int NumberOfSeats { get; set; }
    public List<Seat> Seats { get; set; }
    public Cinema? Cinema { get; set; } 

    public Hall(int hallNumber, int numberOfSeats, List<Seat> seats, Cinema? cinema = null)
    {
        HallNumber = hallNumber;
        NumberOfSeats = numberOfSeats;
        Seats = seats;
        Cinema = cinema;
        ValidateSeats();
    }

    public bool HasAvailableSeats()
    {
        if (Seats == null)
        {
            throw new HallException("The hall's seats list is not initialized.");
        }
        if (Seats.Count == 0)
        {
            throw new HallException("The hall has no seats.");
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
        if (Seats.Count < 20 || Seats.Count > 100)
        {
            throw new ValidationException($"The number of seats in the hall must be between 20 and 100. Current count: {Seats.Count}");
        }
    }
    public void SetCinema(Cinema cinema)
    {
        Cinema = cinema;
    }
}