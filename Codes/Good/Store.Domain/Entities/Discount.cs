namespace Store.Domain.Entities;

public class Discount : Entity
{
    public decimal Amount { get; private set; }
    public DateTime ExpireDate { get; private set; }

    public Discount(decimal amount, DateTime expireDate)
    {
        Amount = amount;
        ExpireDate = expireDate;
    }

    private bool IsValid()
        => DateTime.Compare(DateTime.Now, ExpireDate) < 0 && Amount > 0;
    
    public decimal Value()
        => IsValid() ? Amount : 0;
}