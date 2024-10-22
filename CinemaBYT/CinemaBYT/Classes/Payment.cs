public class Payment
{
    public PaymentType Type { get; set; }
    public DateTime PaymentDate { get; set; }
    public int MaxTicketPerPayment { get; set; } = 5;

    public Payment(PaymentType type, DateTime paymentDate, int maxTicketPerPayment)
    {
        Type = type;
        PaymentDate = paymentDate;
        MaxTicketPerPayment = maxTicketPerPayment;
    }
}

public enum PaymentType
{
    Blik,
    Card,
    GiftCard
}