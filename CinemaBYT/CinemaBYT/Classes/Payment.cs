public class Payment
{
    public PaymentType Type { get; set; }
    public DateTime PaymentDate { get; set; }
    public int MaxTicketPerPayment { get; set; } = 5;
}

public enum PaymentType
{
    Blik,
    Card,
    GiftCard
}