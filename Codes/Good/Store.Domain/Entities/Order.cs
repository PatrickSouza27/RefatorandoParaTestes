using Flunt.Validations;
using Store.Domain.Entities.Enums;

namespace Store.Domain.Entities;

public class Order : Entity
{
    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public IList<OrderItem> Items { get; private set; }
    public Discount Discount { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public EOrderStatus Status { get; private set; }

    public Order(Customer customer, decimal deliveryFee, Discount discount)
    {
        
        AddNotifications(new Contract()
            .Requires()
            .IsNotNull(Customer,"Customer", "Customer is required"));
        
        Customer = customer;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString()[..8];
        Status = EOrderStatus.WaitingPayment;
        Items = new List<OrderItem>();
        Discount = discount;
        DeliveryFee = deliveryFee;
    }

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);
        if(item.Valid)
            Items.Add(item);
    }
    public decimal Total()
    {
        var total = Items.Sum(x => x.Total());
        total += DeliveryFee;
        total -= Discount.Value();
        return total;
    }

    public void Pay(decimal amount)
    {
        if (amount == Total())
            Status = EOrderStatus.WaitingDelivery;
    }

    public void Cancelled()
        => Status = EOrderStatus.Cancelled;
}