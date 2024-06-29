using Microsoft.Extensions.DependencyInjection;
using MyShop.Domain.Models;

namespace MyShop.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services) 
        {
            services.AddTransient<ShoppingContext>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
