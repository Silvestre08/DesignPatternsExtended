using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure
{
    public interface IRepository<T>
    {
        T Add(T entity);

        T Update(T entity);

        T Get(Guid id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void SaveChanges();
    }
}
