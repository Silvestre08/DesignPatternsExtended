using MyShop.Domain.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MyShop.Infrastructure
{
    internal class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ShoppingContext context) : base(context)
        {
             
        }

        public override Product Update(Product entity)
        {
            var product = Context.Products.Single(p => p.ProductId == entity.ProductId);
            product.Name = entity.Name;
            product.Price = entity.Price;
            return base.Update(entity);
        }
    }
}
