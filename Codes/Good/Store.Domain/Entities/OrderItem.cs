using Flunt.Validations;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public Product? Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Product product, int quantity)
    {
        AddNotifications(new Contract()
            .Requires()
            .IsNotNull(product, "Product", "Product Invalid")
            .IsGreaterThan(quantity, 0, "Quantity", "Quantity Invalid"));
        
        
        
        
        Product = product;
        Price = Product != null ? product.Price : 0;
        Quantity = quantity;
    }
    
    public decimal Total() => Price * Quantity;
}