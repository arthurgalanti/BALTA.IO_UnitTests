using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
        private readonly Customer _customer = new Customer("Arthur Galanti", "arthur@galanti.dev");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));


        [TestMethod]
        public void DadoUmNovoPedidoValidoEleDeveGerarUmNumeroCom8Caracteres()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        public void DadoUmNovoPedidoSeuStatusDeveSerAguardandoPagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        public void DadoUmPagamentoDoPedidoSeuStatusDeveSerAguardandoEntrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1); // Total deve ser 10
            order.Pay(10);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        public void DadoUmPedidoCanceladoSeuStatusDeveSerCancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        public void DadoUmNovoItemSemProdutoOmesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 10);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        public void DadoUmNovoItemComQuantidadeZeroOuMenorOMesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }
        
        [TestMethod]
        public void DadoUmPedidoSemClienteOMesmoDeveSerInvalido()
        {
            var order = new Order(null, 10, _discount);
            Assert.AreEqual(order.IsValid, false);
        }
}