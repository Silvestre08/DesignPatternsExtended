using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure;
using System;
using System.Linq;

namespace MyShop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ShoppingContext context;

        public CustomerController()
        {
            context = new ShoppingContext();
        }

        public IActionResult Index(Guid? id)
        {
            if (id == null)
            {
                var customers = context.Customers.ToList();

                return View(customers);
            }
            else
            {
                var customer = context.Customers.Find(id.Value);

                return View(new[] { customer });
            }
        }
    }
}
