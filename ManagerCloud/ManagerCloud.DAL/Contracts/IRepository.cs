using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ManagerCloud.DAL.Contracts
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = default(Expression<Func<T, bool>>));
        int GetId(Expression<Func<T, bool>> predicate = default(Expression<Func<T, bool>>));
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}
