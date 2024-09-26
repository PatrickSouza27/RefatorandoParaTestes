using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeDiscountRepository : IDiscountRepository
{
    public Discount? Get(string code)
        => code switch
        {
            "12345" => new Discount(10, DateTime.Now.AddDays(5)),
            "11111" => new Discount(10, DateTime.Now.AddDays(-5)),
            _ => null
        };
    
}