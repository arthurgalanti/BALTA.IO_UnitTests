using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IDeliveryFeeRepository _deliveryFeeRepository;

    private readonly IDiscountRepository _discountRepository;

    private readonly IProductRepository _productRepository;

    private readonly IOrderRepository _orderRepository;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _orderRepository = new FakeOrderRepository();
        _productRepository = new FakeProductRepository();
    }

    [TestMethod]
    public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
    {
        var command = new CreateOrderCommand();
        command.Customer = null;
        command.ZipCode = "89787897987";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );
        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, true);
    }
    
    [TestMethod]
    public void DadoUmCEPInvalidoOPedidoDeveSerGeradoNormalmente()
    {
        var command = new CreateOrderCommand();
        command.Customer = "12345678911";
        command.ZipCode = "89787897987";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );
        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, true);
    }
    
    [TestMethod]
    public void DadoUmPromoCodeInexistenteOPedidoDeveSerGeradoNormalmente()
    {
        var command = new CreateOrderCommand();
        command.Customer = "12345678911";
        command.ZipCode = "123311080";
        command.PromoCode = "984616516541651";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );
        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, true);
    }
    
    [TestMethod]
    public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
    {
        var command = new CreateOrderCommand();
        command.Customer = "12345678911";
        command.ZipCode = "123311080";
        command.PromoCode = "12345678";
        command.Validate();
        
        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );
        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
    {
        var command = new CreateOrderCommand();
        command.Customer = "";
        command.ZipCode = "123311080";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();
        
        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );
        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, false);
    }
    
    [TestMethod]
    public void DadoUmComandoValidoOPedidoDeveSerGerado()
    {
        var command = new CreateOrderCommand();
        command.Customer = "12345678911";
        command.ZipCode = "123311080";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );
        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, true);
    }
}