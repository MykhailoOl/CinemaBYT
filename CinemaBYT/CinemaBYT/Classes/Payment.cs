using System;
using CinemaBYT.Exceptions;

public class Payment
{
    private PaymentType _type;
    private DateTime _paymentDate;
    private int _maxTicketPerPayment = 5;

    public PaymentType Type
    {
        get => _type;
        set => _type = value;
    }

    public DateTime PaymentDate
    {
        get => _paymentDate;
        set => _paymentDate = value;
    }

    public int MaxTicketPerPayment
    {
        get => _maxTicketPerPayment;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxTicketPerPayment), "Max tickets per payment must be positive.");
            }
            _maxTicketPerPayment = value;
        }
    }

    public Payment(PaymentType type, DateTime paymentDate, int maxTicketPerPayment = 5)
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

        return hasLoyaltyCard ? price * 0.9 : price;  // 10% discount for loyalty card holders
    }
}

public enum PaymentType
{
    Blik,
    CreditCard,
    GiftCard
}
