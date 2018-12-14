using ManagerCloud.Core.CustomExceptions.ModelObjectException;
using ManagerCloud.Core.Helpers;
using ManagerCloud.DAL.Contracts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace ManagerCloud.BL.UnitOfWorks
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

        public TModel TryGet(Expression<Func<TModel, bool>> searchExpression)
        {
            var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);

            var newExpression = searchExpression.Project<TModel, TEntity>();

            _locker.EnterReadLock();

            try
            {
                var searchedElement = repositoryElement.Get().FirstOrDefault(newExpression);
                return searchedElement != null ? ToModel.Invoke(searchedElement) : null;
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public TEntity TryAdd(TModel modelElement, Expression<Func<TModel, bool>> searchExpression)
        {
            TEntity newEntityElement;
            _locker.EnterWriteLock();

            try
            {
                var searchedElement = TryGet(searchExpression);
                if (searchedElement != null)
                {
                    newEntityElement = ToEntity(searchedElement);
                    return newEntityElement;
                }

                var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);
                newEntityElement = ToEntity(modelElement);
                repositoryElement.Add(newEntityElement);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ObjectAdditionException(modelElement);
            }
            finally
            {
                _locker.ExitWriteLock();
            }

            return newEntityElement;
        }

        public void TryUpdate(TEntity entityElement, Expression<Func<TModel, bool>> searchExpression)
        {
            _locker.EnterWriteLock();
            try
            {
                if (TryGet(searchExpression) == null)
                {
                    return;
                }
                var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);
                repositoryElement.Update(entityElement);
                _context.SaveChanges();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void TryRemove(TEntity entityElement, Expression<Func<TModel, bool>> searchExpression)
        {
            _locker.EnterWriteLock();
            try
            {
                if (TryGet(searchExpression) == null)
                {
                    return;
                }

                var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);
                repositoryElement.Remove(entityElement);
                _context.SaveChanges();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public TEntity TryEntityGet(Expression<Func<TModel, bool>> searchExpression)
        {
            var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);

            var newExpression = searchExpression.Project<TModel, TEntity>();

            _locker.EnterReadLock();

            try
            {
                var entityElement = repositoryElement.Get().FirstOrDefault(newExpression);
                return entityElement;
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }
    }
}
