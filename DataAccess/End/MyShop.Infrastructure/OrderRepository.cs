﻿using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyShop.Infrastructure
{
    internal class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(ShoppingContext context) : base(context)
        {
        }

        public override IEnumerable<Order> GetAll()
        {
            return Context.Orders.Include(o => o.LineItems)
                .ThenInclude(l => l.Product).ToList();
        }

        public override IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            // eagrly loaded to include all the line items
            return Context.Orders.Include(o => o.LineItems)
                .ThenInclude(l => l.Product).Where(predicate).ToList();
        }

        public override Order Update(Order entity)
        {
            var order = Context.Orders.Include(o => o.LineItems)
          .ThenInclude(l => l.Product).Single(o => o.OrderId == entity.OrderId);
            order.OrderDate = entity.OrderDate;
            order.LineItems = entity.LineItems;
            return base.Update(entity);
        }
    }
}
