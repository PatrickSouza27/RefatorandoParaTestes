using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer? Get(string document) => document is "123" ? new Customer("Bruce Wayne", "batman@gmail.com") : null;
}