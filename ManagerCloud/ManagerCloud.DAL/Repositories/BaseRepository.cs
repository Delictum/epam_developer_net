using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ManagerCloud.DAL.Contracts;

namespace ManagerCloud.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class 
    {
        protected DbContext Context { get; set; }
        protected static DbSet<T> DbSet { get; set; }

        protected BaseRepository()
        {
        }

        protected BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public static IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null ? DbSet.Where(predicate) : DbSet;
        }

        IQueryable<T> IRepository<T>.Get(Expression<Func<T, bool>> predicate)
        {
            return Get(predicate);
        }

        public abstract int GetId(Expression<Func<T, bool>> predicate);

        public void Add(T item)
        {
            DbSet.Add(item);
        }
        
        public void Update(T item)
        {
            DbSet.Attach(item);
            Context.Entry(item).State = EntityState.Modified;
        }

        public void Remove(T item)
        {
            if (Context.Entry(item).State == EntityState.Detached)
            {
                DbSet.Attach(item);
            }
            DbSet.Remove(item);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
