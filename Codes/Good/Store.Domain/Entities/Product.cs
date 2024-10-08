﻿namespace Store.Domain.Entities;

public class Product : Entity
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
    
    public Product(string title, decimal price)
    {
        Title = title;
        Price = price;
        Active = true;
    }
    
    public Product Deactivate()
    {
        Active = false;
        return this;
    }
}