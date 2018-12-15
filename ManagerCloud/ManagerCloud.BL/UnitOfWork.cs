using ManagerCloud.Core.CustomExceptions.ModelObjectException;
using ManagerCloud.Core.Helpers;
using ManagerCloud.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using ManagerCloud.BL.Models;
using ManagerCloud.DAL;
using ManagerCloud.DAL.Repositories;

namespace ManagerCloud.BL
{
    public class UnitOfWork : IDisposable
    {
        protected DbContext Context { get; }
        public IRepository<EF.Client> ClientRepository { get; set; }
        public IRepository<EF.Item> ItemRepository { get; set; }
        public IRepository<EF.DataSource> DataSourceRepository { get; set; }
        public IRepository<EF.Sale> SaleRepository { get; set; }
        public IDictionary<Type, ReaderWriterLockSlim> Lockers { get; }

        public UnitOfWork(DbContext context, IDictionary<Type, ReaderWriterLockSlim> lockers)
        {
            Context = context;
            InitializeRepository();
            Lockers = lockers;
        }

        private void InitializeRepository()
        {
            ClientRepository = new RepositoryFactory().CreateInstanceClient(Context);
            ItemRepository = new RepositoryFactory().CreateInstanceItem(Context);
            DataSourceRepository = new RepositoryFactory().CreateInstanceDataSource(Context);
            SaleRepository = new RepositoryFactory().CreateInstanceSale(Context);
        }

        private ReaderWriterLockSlim ResolveLocker(Type modelType)
        {
            return Lockers[modelType];
        }

        public TEntity GetEntity<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            var newSearchExpression = searchExpression.Project<TModel, TEntity>();

            ResolveLocker(typeof(TModel)).EnterReadLock();
            try
            {
                return repository.Get().FirstOrDefault(newSearchExpression);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitReadLock();
            }
        }

        public int GetEntityId<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            var newSearchExpression = searchExpression.Project<TModel, TEntity>();

            ResolveLocker(typeof(TModel)).EnterReadLock();
            try
            {
                return repository.GetId(newSearchExpression);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitReadLock();
            }
        }

        public void TryAddClient(Client client, Expression<Func<Client, bool>> searchExpression)
        {
            ResolveLocker(typeof(Client)).EnterWriteLock();
            try
            {
                var searchedClient = GetEntity(searchExpression, ClientRepository);
                if (searchedClient != null)
                {
                    return;
                }
                
                ClientRepository.Add(new EF.Client { FirstName = client.FirstName, LastName = client.LastName});
            }
            catch (Exception)
            {
                throw new ObjectAdditionException(client);
            }
            finally
            {
                ResolveLocker(typeof(Client)).ExitWriteLock();
            }
        }

        public void TryAddItem(Item item, Expression<Func<Item, bool>> searchExpression)
        {
            ResolveLocker(typeof(Item)).EnterWriteLock();
            try
            {
                var searchedItem = GetEntity(searchExpression, ItemRepository);
                if (searchedItem != null)
                {
                    return;
                }

                ItemRepository.Add(new EF.Item { Name = item.Name});
            }
            catch (Exception)
            {
                throw new ObjectAdditionException(item);
            }
            finally
            {
                ResolveLocker(typeof(Item)).ExitWriteLock();
            }
        }

        public void TryAddDataSource(DataSource dataSource, Expression<Func<DataSource, bool>> searchExpression)
        {
            ResolveLocker(typeof(DataSource)).EnterWriteLock();
            try
            {
                var searchedDataSource = GetEntity(searchExpression, DataSourceRepository);
                if (searchedDataSource != null)
                {
                    return;
                }

                DataSourceRepository.Add(new EF.DataSource { FileName = dataSource.FileName});
            }
            catch (Exception)
            {
                throw new ObjectAdditionException(dataSource);
            }
            finally
            {
                ResolveLocker(typeof(DataSource)).ExitWriteLock();
            }
        }

        public void VerifyInContext<T>(List<T> entities) where T : class 
        {
            if (entities == null)
                return;

            var localStorage = Context.Set<T>().Local;
            foreach (var entity in entities)
            {
                if (localStorage.All(e => !ReferenceEquals(e, entity)))
                {
                    throw new Exception(string.Format("Entity {0} must be in context", typeof(T).Name));
                }
            }
        }

        public void TryAddSale(Sale sale, Expression<Func<Sale, bool>> searchExpression)
        {
            ResolveLocker(typeof(Sale)).EnterWriteLock();
            try
            {
                
                var searchedSale = GetEntity(searchExpression, SaleRepository);
                if (searchedSale != null)
                {
                    return;
                }

                Expression<Func<Client, bool>> clientSearchCriteria = x =>
                    x.FirstName == sale.Client.FirstName && x.LastName == sale.Client.LastName;
                var clientId = GetEntityId(clientSearchCriteria, ClientRepository);

                Expression<Func<Item, bool>> itemSearchCriteria = x =>
                    x.Name == sale.Item.Name;
                var itemId = GetEntityId(itemSearchCriteria, ItemRepository);

                Expression<Func<DataSource, bool>> dataSourcetSearchCriteria = x =>
                    x.FileName == sale.DataSource.FileName;
                var dataSourceId = GetEntityId(dataSourcetSearchCriteria, DataSourceRepository);

                SaleRepository.Add(new EF.Sale { Date = sale.Date, SaleSum = sale.SaleSum, ClientId = clientId, DataSourceId = dataSourceId, ItemId = itemId });
            }
            catch (Exception)
            {
                throw new ObjectAdditionException(sale);
            }
            finally
            {
                ResolveLocker(typeof(Sale)).ExitWriteLock();
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void AsNoLazyLoading()
        {
            Context.Configuration.LazyLoadingEnabled = false;
            Context.Configuration.ProxyCreationEnabled = false;
        }

        //public void TryUpdate(TEntity entityElement, Expression<Func<TModel, bool>> searchExpression)
        //{
        //    _locker.EnterWriteLock();
        //    try
        //    {
        //        if (Get(searchExpression) == null)
        //        {
        //            return;
        //        }
        //        var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);
        //        repositoryElement.Update(entityElement);
        //        _context.SaveChanges();
        //    }
        //    finally
        //    {
        //        _locker.ExitWriteLock();
        //    }
        //}

        //public void TryRemove(TEntity entityElement, Expression<Func<TModel, bool>> searchExpression)
        //{
        //    _locker.EnterWriteLock();
        //    try
        //    {
        //        if (Get(searchExpression) == null)
        //        {
        //            return;
        //        }

        //        var repositoryElement = _repoFactory.CreateInstance<TEntity>(_context);
        //        repositoryElement.Remove(entityElement);
        //        _context.SaveChanges();
        //    }
        //    finally
        //    {
        //        _locker.ExitWriteLock();
        //    }
        //}

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
