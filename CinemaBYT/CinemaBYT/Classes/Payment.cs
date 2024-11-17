using System;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

public class Payment
{
    private PaymentType _type;
    private DateTime _paymentDate;
    private int _maxTicketPerPayment = 5;

    [DisallowNull]
    public PaymentType Type
    {
        get => _type;
        set => _type = value; 
    }

    [DisallowNull]
    public DateTime PaymentDate
    {
        get => _paymentDate;
        set
        {
            if (value == default)
            {
                throw new ArgumentNullException(nameof(PaymentDate), "Payment date cannot be null or default.");
            }
            _paymentDate = value;
        }
    }

    [DisallowNull]
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
        PaymentDate = paymentDate == default
            ? throw new ArgumentNullException(nameof(paymentDate), "Payment date cannot be default.")
            : paymentDate;
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
    public override bool Equals(object obj)
    {
        if (obj is Payment other)
        {
            // Compare the essential properties of the Payment class.
            return Type == other.Type &&
                   PaymentDate == other.PaymentDate &&
                   MaxTicketPerPayment == other.MaxTicketPerPayment;
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of the essential properties.
        int hashCode = Type.GetHashCode();
        hashCode = (hashCode * 397) ^ PaymentDate.GetHashCode();
        hashCode = (hashCode * 397) ^ MaxTicketPerPayment.GetHashCode();

        return hashCode;
    }

}

public enum PaymentType
{
    Blik,
    CreditCard,
    GiftCard
}
