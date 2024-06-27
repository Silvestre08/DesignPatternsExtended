using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure
{
    internal abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ShoppingContext Context;

        public GenericRepository(ShoppingContext context)
        {
            Context = context;
        }

        public T Add(T entity) => Context.Add(entity).Entity;

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
           return Context.Set<T>().AsQueryable().Where(predicate);
        }

        public T Get(Guid id)
        {
           return Context.Find<T>(id);
        }

        public IEnumerable<T> GetAll() 
            => Context.Set<T>().ToList();

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual T Update(T entity) => Context.Update(entity).Entity;
    }
}
