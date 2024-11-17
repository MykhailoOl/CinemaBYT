using System;

public class OwnsLoyaltyCard : Person
{
    private DateTime _startDate;
    private DateTime _expireDate;
    private decimal _discount;

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(StartDate), "Start date cannot be in the future.");
            }
            _startDate = value;
        }
    }

    public DateTime ExpireDate
    {
        get => _expireDate;
        set
        {
            if (value <= StartDate)
            {
                throw new ArgumentOutOfRangeException(nameof(ExpireDate), "Expire date must be after the start date.");
            }
            _expireDate = value;
        }
    }

    public decimal Discount
    {
        get => _discount * 100;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(Discount), "Discount must be between 0 and 100.");
            }
            _discount = value / 100;
        }
    }

    public OwnsLoyaltyCard(String name,String email, DateTime birthDate,String pesel,DateTime startDate, DateTime expireDate, decimal discount)
        : base(name, email, birthDate, pesel)
    {
        StartDate = startDate;
        ExpireDate = expireDate;
        Discount = discount;
    }

    public OwnsLoyaltyCard()
    {
    }
    public override bool Equals(object obj)
    {
        if (obj is OwnsLoyaltyCard other)
        {
            // Compare the essential properties of the OwnsLoyaltyCard class.
            return base.Equals(other) && // Ensures the comparison also includes properties from Person (base class)
                   StartDate == other.StartDate &&
                   ExpireDate == other.ExpireDate &&
                   Discount == other.Discount;
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Use a combination of the properties for a good hash code.
        int hashCode = base.GetHashCode(); // Start with base class hash code (Person)
        hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
        hashCode = (hashCode * 397) ^ ExpireDate.GetHashCode();
        hashCode = (hashCode * 397) ^ Discount.GetHashCode();
    
        return hashCode;
    }

}
