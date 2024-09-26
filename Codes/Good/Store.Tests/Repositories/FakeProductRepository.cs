using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeProductRepository : IProductRepository
{
    public IEnumerable<Product> Get(IEnumerable<Guid> ids)
    {
        return new List<Product>
        {
            new Product("Product 1", 10),
            new Product("Product 2", 10),
            new Product("Product 3", 10),
            new Product("Product 4", 10),
            new Product("Product 5", 10)
        };
    }
}