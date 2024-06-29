using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Web.Controllers;
using MyShop.Web.Models;
using NSubstitute;

namespace Controller.Tests
{
    public class OrderControllersTests
    {
        private OrderController _orderController;
        private IRepository<Order> _orderRepository;
        private IRepository<Product> _productRepository;

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IRepository<Order>>();
            _productRepository = Substitute.For<IRepository<Product>>();
            _orderController = new OrderController(_orderRepository, _productRepository);
        }

        [Test]
        public void CreateOrderTest()
        {
            // Assemble
            var order = new CreateOrderModel
            {
                Customer = new CustomerModel { City = "Porto", Country = "Portugal", Name = "Celso", PostalCode = "4450", ShippingAddress = "Abc" },
                LineItems = new List<LineItemModel> { new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 2 } },
            };

            //Act
            _orderController.Create(order);

            //Assert
            _orderRepository.Received(1).Add(Arg.Any<Order>());
        }
    }
}