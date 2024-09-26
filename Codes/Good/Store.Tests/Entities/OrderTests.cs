using Store.Domain.Entities;
using Store.Domain.Entities.Enums;

namespace Store.Tests;

[TestClass]
public class OrderTests
{
    
    private readonly Customer _customer = new Customer("Bruce Wayne", "brucewayne@gmail.com");
    private readonly Product _product = new Product("Product 1", 10);
    private readonly Discount _discount = new Discount(0, DateTime.Now.AddDays(5));
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_novo_pedido_ele_deve_gerar_um_numero_com_8_caracteres()
    {
        var order = new Order(_customer, 0, null);
        Assert.AreEqual(8, order.Number.Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
    {
        var order = new Order(_customer, 0, _discount);
        Assert.AreEqual(EOrderStatus.WaitingPayment, order.Status);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_pagamento_do_pedido_seu_status_deve_ser_aguardando_entrega()
    {
        var order = new Order(_customer, 0, _discount);
        order.AddItem(_product, 1); // Total deve ser 10
        order.Pay(10);
        
        Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
    {
        var order = new Order(_customer, 0, _discount);
        order.Cancelled();
        
        Assert.AreEqual(EOrderStatus.Cancelled, order.Status);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
    {
        
        var order = new Order(_customer, 0, _discount);
        order.AddItem(null, 0);
        
        Assert.AreEqual(0, order.Items.Count);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_novo_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
    {
        var order = new Order(_customer, 0, _discount);
        order.AddItem(_product, -1);
        
        Assert.AreEqual(order.Items.Count, 0);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50()
    {
        var order = new Order(_customer, 10, _discount);
        //40 do produto + 10 da taxa de entrega
        
        order.AddItem(_product, 4);
        
        Assert.AreEqual(order.Total(), 50);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
    {
        var discount = new Discount(10, DateTime.Now.AddDays(-5));
        // disconto invalido, não deve ser aplicado
        var order = new Order(_customer, 0, discount);
        // 0 reais do frete
        order.AddItem(_product, 6);
        // 6 produtos de 10 reais cada = 60
        Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_60()
    {
        var order = new Order(_customer, 10, new Discount(-2, DateTime.Now));
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
    {
        var order = new Order(_customer, 0, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
    {
        var order = new Order(_customer, 10, new Discount(0, DateTime.Now));
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
    {
        var order = new Order(null, 10, _discount);
        Assert.AreEqual(order.Valid, false);
    }

}