using Store.Domain.Entities;

namespace Store.Tests.Entities;

[TestClass]
public class DiscountTests
{
    private readonly Customer _customer = new Customer("Arthur Galanti", "arthur@galanti.dev");
    private readonly Product _product = new Product("Produto 1", 10, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));
    
    [TestMethod]
    public void DadoUmDescontoExpiradoOValorDoPedidoDeveSer60()
    {
        var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5));
        var order = new Order(_customer, 10, expiredDiscount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    public void DadoUmDescontoDe10OValorDoPedidoDeveSer50()
    {
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    public void DadoUmaTaxaDeEntregaDe10OValorDoPedidoDeveSer60()
    {
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 6);
        Assert.AreEqual(order.Total(), 60);
    }
}