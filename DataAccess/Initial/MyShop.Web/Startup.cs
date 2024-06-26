﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyShop.Domain.Models;
using MyShop.Infrastructure;

namespace MyShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            CreateInitialDatabase();

            services.AddTransient<ShoppingContext>();
       
        }

        public void CreateInitialDatabase()
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           
        }
    }
}
