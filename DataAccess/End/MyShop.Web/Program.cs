using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyShop.Domain.Models;
using MyShop.Infrastructure;

namespace MyShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRepositories();
            CreateInitialDatabase();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Order}/{action=Index}");

            app.Run();
        }


        public static void CreateInitialDatabase()
        {
            using (var context = new ShoppingContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var camera = new Product { Name = "Canon EOS 70D", Price = 599m };
                var microphone = new Product { Name = "Shure SM7B", Price = 245m };
                var light = new Product { Name = "Key Light", Price = 59.99m };
                var phone = new Product { Name = "Android Phone", Price = 259.59m };
                var speakers = new Product { Name = "5.1 Speaker System", Price = 799.99m };

                context.Products.Add(camera);
                context.Products.Add(microphone);
                context.Products.Add(light);
                context.Products.Add(phone);
                context.Products.Add(speakers);

                context.SaveChanges();
            }
        }

    }
}
