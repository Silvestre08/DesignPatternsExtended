using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Web.Models;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;

namespace MyShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWOrk = unitOfWork;
        }

        public IActionResult Index()
        {
            var orders = _unitOfWOrk.OrderRepository.GetAll();

            return View(orders);
        }

        public IActionResult Create()
        {
            var products = _unitOfWOrk.ProductRepository.GetAll();

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderModel model)
        {
            if (!model.LineItems.Any()) return BadRequest("Please submit line items");

            if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");
            var customer = _unitOfWOrk.CustomerRepository.Find(c => c.Name == model.Customer.Name).FirstOrDefault();

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = model.Customer.Name,
                    ShippingAddress = model.Customer.ShippingAddress,
                    City = model.Customer.City,
                    PostalCode = model.Customer.PostalCode,
                    Country = model.Customer.Country
                };
                _unitOfWOrk.CustomerRepository.Add(customer);
            }
            else
            {
                customer.Name = model.Customer.Name;
                customer.ShippingAddress = model.Customer.ShippingAddress;
                customer.City = model.Customer.City;
                customer.PostalCode = model.Customer.PostalCode;
                customer.Country = model.Customer.Country;
                _unitOfWOrk.CustomerRepository.Update(customer);
            }

            var order = new Order
            {
                LineItems = model.LineItems
                    .Select(line => new LineItem { ProductId = line.ProductId, Quantity = line.Quantity })
                    .ToList(),

                Customer = customer
            };

            _unitOfWOrk.OrderRepository.Add(order);

            _unitOfWOrk.SaveChanges();

            return Ok("Order Created");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
