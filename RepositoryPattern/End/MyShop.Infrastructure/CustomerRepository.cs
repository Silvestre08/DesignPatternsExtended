using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure
{
    internal class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ShoppingContext context) 
            : base(context)
        {
        }

        public override Customer Update(Customer entity)
        {
            var customer = Context.Customers.Single(c => c.CustomerId == entity.CustomerId);

            customer.Name = entity.Name;
            customer.City = entity.City;
            customer.PostalCode = entity.PostalCode;
            customer.ShippingAddress = entity.ShippingAddress;
            customer.Country = entity.Country;

            return base.Update(entity);
        }
    }
}
