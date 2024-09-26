using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private IList<Product> _products;

    public ProductQueriesTests(IList<Product> products)
    {
        _products = new List<Product>();
        _products.Add(new Product("Produto 01", 10));
        _products.Add(new Product("Produto 02", 20));
        _products.Add(new Product("Produto 03", 30));
        _products.Add(new Product("Produto 04", 40).Deactivate());
        _products.Add(new Product("Produto 05", 50).Deactivate());
    }


    [TestMethod]
    [TestCategory("Queries")]
    public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
        //ou _products.AsQueryable().Where(x=> x.Active);
        
        Assert.AreEqual(3, result.Count());
    }

    public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        
        Assert.AreEqual(2, result.Count());
    }
}