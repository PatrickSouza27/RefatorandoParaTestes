namespace Store.Domain.Repositories;

public interface IDeliveryFee
{
    decimal Get(string zipCode);
}