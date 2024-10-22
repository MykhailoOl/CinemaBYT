public class OwnsLoyaltyCard
{
    public DateTime StartDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public decimal Discount { get; set; }

    public OwnsLoyaltyCard(DateTime startDate, DateTime expireDate, decimal discount)
    {
        StartDate = startDate;
        ExpireDate = expireDate;
        Discount = discount;
    }
}
