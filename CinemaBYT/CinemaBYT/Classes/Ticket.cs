using System.Net.Sockets;
using CinemaBYT.Exceptions;


public class Ticket
{
    public int SeatNumber { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public TicketType Type { get; set; }
    public Session Session { get; set; }
    public Seat Seat { get; set; }
    public Person Person { get; set; }

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
        try
        {
            if (Seat == null)
            {
                throw new TicketException("Seat is not initialized.");
            }

            if (Seat.ReserveSeat())
            {
                Console.WriteLine("Ticket purchased successfully!");
                return true;
            }
            Console.WriteLine("Seat is already reserved."); 
            return false;
        }
        catch (TicketException ex)
        {
            Console.WriteLine("Ticket purchase error: " + ex.Message);
            return false;
        }
    }

    public bool RefundTicket()
    {
        try
        {
            if (Seat == null)
            {
                throw new TicketException("Seat is not initialized.");
            }

            if (CanRefund())
            {
                Seat.IsAvailable = true;
                Console.WriteLine("Ticket refunded successfully.");
                return true;
            }
            Console.WriteLine("Ticket cannot be refunded."); 
            return false;
        }
        catch (TicketException ex)
        {
            Console.WriteLine("Refund error: " + ex.Message);
            return false;
        }
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