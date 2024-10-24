using CinemaBYT.Exceptions;

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
    public static double LoyaltyDiscount(double price, bool hasLoyaltyCard)
    {
        if (price <= 0)
        {
            throw new PaymentException("Price must be a positive value.");
        }

        if (hasLoyaltyCard)
        {
            return price * 0.9;  // 10% discount for loyalty card holders
        }

        return price;
    }
}

public enum PaymentType
{
    Blik,
    Card,
    GiftCard
}