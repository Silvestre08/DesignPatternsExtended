using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Ghosts;
using MyShop.Infrastructure.Lazy.Proxies;
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

        public override Customer Get(Guid id)
        {

            var customerId = Context.Customers.Single(c => c.CustomerId == id).CustomerId;
            return new GhostCustomer(() => base.Get(id)){ CustomerId = customerId };
        }

        public override IEnumerable<Customer> GetAll()
        {

            return base.GetAll().Select(MapToProxy);
            // see customer lazy. left it here as a reference
            //return base.GetAll().Select( c => 
            //{
            //    c.ProfilePictureValueHolder = new Lazy<byte[]>(ProfilePictureService.GetFor(c.Name));
            //    return c;
            //});
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

        private CustomerProxy MapToProxy(Customer customer) 
        {
            return new CustomerProxy
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                City = customer.City,
                PostalCode = customer.PostalCode,
                ShippingAddress = customer.ShippingAddress,
                Country = customer.Country,

            };
        }
    }
}
