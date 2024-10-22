using System.Net.Sockets;

public class Ticket
{
    public int SeatNumber { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public TicketType Type { get; set; }
    public Session Session { get; set; }
    public Seat Seat { get; set; }

    public Ticket(int seatNumber, decimal price, DateTime purchaseDate, TicketType type, Session session,Seat seat)
    {
        SeatNumber = seatNumber;
        Price = price;
        PurchaseDate = purchaseDate;
        Type = type;
        Session = session;
        Seat = seat;
    }
    public bool BuyTicket()
    {
        if (Seat.ReserveSeat())
        {
            Console.WriteLine("Ticket purchased successfully!");
            return true;
        }
        Console.WriteLine("Seat is already reserved.");
        return false;
    }
    public bool RefundTicket()
    {
        if (CanRefund())
        {
            Seat.IsAvailable=true; 
            Console.WriteLine("Ticket refunded successfully.");
            return true;
        }
        Console.WriteLine("Ticket cannot be refunded.");
        return false;
    }
    private bool CanRefund()
    {
        TimeSpan timeDifference = DateTime.Now - PurchaseDate;
        return timeDifference.TotalHours < 24;
    }
}
public enum TicketType
{
    Adult,
    Senior,
    Child
}