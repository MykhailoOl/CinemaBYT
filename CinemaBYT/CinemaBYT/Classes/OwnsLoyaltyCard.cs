using System;

public class OwnsLoyaltyCard : Person
{
    private DateTime _startDate;
    private DateTime _expireDate;

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
        get
        {
            TimeSpan timeToExpire = ExpireDate - DateTime.Now;

            if (timeToExpire.TotalDays <= 1)
            {
                return 0.2m * 100; // 20% discount
            }

            if (timeToExpire.TotalDays > 30)
            {
                return 0m; // No discount if more than 30 days remain
            }

            // Calculate a proportional discount based on the remaining days
            decimal proportionalDiscount = (decimal)(1 - (timeToExpire.TotalDays / 30)) * 0.2m;
            return proportionalDiscount * 100; // Convert to percentage
        }
    }

    public OwnsLoyaltyCard(string name, string email, DateTime birthDate, string pesel, DateTime startDate, DateTime expireDate)
        : base(name, email, birthDate, pesel)
    {
        StartDate = startDate;
        ExpireDate = expireDate;
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
