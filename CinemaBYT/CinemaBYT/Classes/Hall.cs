using System.ComponentModel.DataAnnotations;

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
        foreach (var seat in Seats)
        {
            if (seat.IsAvailable) return true;
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