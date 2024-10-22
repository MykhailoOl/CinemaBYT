using System.Net.Sockets;

public class Ticket
{
    public int SeatNumber { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public TicketType Type { get; set; }
    public Session Session { get; set; }
}
public enum TicketType
{
    Adult,
    Senior,
    Child
}