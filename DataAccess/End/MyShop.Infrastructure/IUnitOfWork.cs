using MyShop.Domain.Models;
using System.Runtime.InteropServices;

namespace MyShop.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Customer> CustomerRepository { get; }
        
        IRepository<Order> OrderRepository{ get; }

        IRepository<Product> ProductRepository { get; }

        void SaveChanges();
    }
}
