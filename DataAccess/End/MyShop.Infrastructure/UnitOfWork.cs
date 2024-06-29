using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private ShoppingContext _shoppingContext;

        private IRepository<Customer> _costumerRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<Product> _productRepository;

        public IRepository<Customer> CustomerRepository 
            => _costumerRepository ?? new CustomerRepository(_shoppingContext);

        public IRepository<Order> OrderRepository 
            => _orderRepository ?? new OrderRepository(_shoppingContext);

        public IRepository<Product> ProductRepository 
            => _productRepository ?? new ProductRepository(_shoppingContext);

        public UnitOfWork(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public void SaveChanges()
        {
            _shoppingContext.SaveChanges();
        }
    }
}
