using System.ComponentModel.DataAnnotations;

public class Hall
{
    public int HallNumber { get; set; }
    public int NumberOfSeats { get; set; }
    [MinLength(20)]
    [MaxLength(100)]
    public List<Seat> Seats { get; set; }
    public Cinema Cinema { get; set; } // Belongs to a Cinema

    public Hall(int hallNumber, int numberOfSeats, List<Seat> seats, Cinema cinema)
    {
        HallNumber = hallNumber;
        NumberOfSeats = numberOfSeats;
        Seats = seats;
        Cinema = cinema;
    }

    public bool HasAvailableSeats()
    {
        foreach (var seat in Seats)
        {
            if (seat.IsAvailable) return true;
        }

        return false;
    }
}