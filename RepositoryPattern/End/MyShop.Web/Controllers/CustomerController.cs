using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using System;
using System.Linq;

namespace MyShop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _custumerRepository;

        public CustomerController(IRepository<Customer> repository)
        {
            _custumerRepository = repository;
        }

        public IActionResult Index(Guid? id)
        {
            if (id == null)
            {
                var customers = _custumerRepository.GetAll();

                return View(customers);
            }
            else
            {
                var customer = _custumerRepository.Get(id.Value);

                return View(new[] { customer });
            }
        }
    }
}
