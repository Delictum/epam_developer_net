using DAL.ManagerCloud.Contracts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace BL.ManagerCloud.UnitOfWorks
{
    public abstract class GenericUnitOfWork<TModel, TEntity> where TEntity : class where TModel : class
    {
        protected readonly DbContext _context;
        protected readonly IRepositoryFactory _repoFactory;
        protected readonly ReaderWriterLockSlim _locker;
        protected abstract Func<TModel, TEntity> ToEntity { get; }
        protected abstract Func<TEntity, TModel> ToModel { get; }

        protected GenericUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
        {
            _context = context;
            _repoFactory = repoFactory;
            _locker = locker;
        }

        public TModel TryGet(Expression<Func<TModel, bool>> searchExpression, bool safeMode = false)
        {
            var entityRepository = _repoFactory.CreateInstance<TEntity>(_context);

            var newExpression = searchExpression.Project<TModel, TEntity>();

            if (safeMode)
            {
                _locker.EnterReadLock();
            }

            try
            {
                var user = entityRepository.Get().FirstOrDefault(newExpression);
                return user != null ? ToModel.Invoke(user) : null;
            }
            finally
            {
                if (safeMode)
                {
                    _locker.ExitReadLock();
                }
            }
        }

        public TEntity TryAdd(TModel customer, Expression<Func<TModel, bool>> searchExpression, bool requiredBlocking)
        {
            TEntity newCustomer;
            _locker.EnterWriteLock();

            try
            {
                var item = TryGet(searchExpression);
                if (item != null)
                {
                    newCustomer = ToEntity(item);
                    return newCustomer;
                }

                var userRepository = _repoFactory.CreateInstance<TEntity>(_context);
                newCustomer = ToEntity(customer);
                userRepository.Add(newCustomer);
                _context.SaveChanges();
            }
            finally
            {
                _locker.ExitWriteLock();
            }

            return newCustomer;
        }

        public void TryUpdate(TEntity customer, Expression<Func<TModel, bool>> searchExpression, bool requiredBlocking)
        {
            if (requiredBlocking) _locker.EnterWriteLock();
            try
            {
                if (TryGet(searchExpression) == null) return;
                var userRepository = _repoFactory.CreateInstance<TEntity>(_context);
                userRepository.Update(customer);
                userRepository.Save();
            }
            finally
            {
                if (requiredBlocking) _locker.ExitWriteLock();
            }
        }

        public void TryRemove(TEntity customer, Expression<Func<TModel, bool>> searchExpression, bool requiredBlocking)
        {
            if (requiredBlocking) _locker.EnterWriteLock();
            try
            {
                if (TryGet(searchExpression) == null) return;

                var userRepository = _repoFactory.CreateInstance<TEntity>(_context);
                userRepository.Remove(customer);
                userRepository.Save();
            }
            finally
            {
                if (requiredBlocking) _locker.ExitWriteLock();
            }
        }

        public TEntity TryEntityGet(Expression<Func<TModel, bool>> searchExpression, bool safeMode = false)
        {
            var entityRepository = _repoFactory.CreateInstance<TEntity>(_context);

            var newExpression = searchExpression.Project<TModel, TEntity>();

            if (safeMode)
            {
                _locker.EnterReadLock();
            }

            try
            {
                var user = entityRepository.Get().FirstOrDefault(newExpression);
                return user;
            }
            finally
            {
                if (safeMode)
                {
                    _locker.ExitReadLock();
                }
            }
        }
    }
}
