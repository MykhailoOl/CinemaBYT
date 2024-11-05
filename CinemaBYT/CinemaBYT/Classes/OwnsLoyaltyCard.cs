using System;

public class OwnsLoyaltyCard
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

    public OwnsLoyaltyCard(DateTime startDate, DateTime expireDate, decimal discount)
    {
        StartDate = startDate;
        ExpireDate = expireDate;
        Discount = discount;
    }
}
