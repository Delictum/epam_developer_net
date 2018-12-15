using ManagerCloud.Core.CustomExceptions.ModelObjectException;
using ManagerCloud.Core.Helpers;
using ManagerCloud.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;

namespace ManagerCloud.BL
{
    internal class DatabaseItemLoader
    {
        private readonly IDictionary<Type, ReaderWriterLockSlim> _lockers;
        private readonly IDbContextFactory _contextFactory;

        public DatabaseItemLoader(IDictionary<Type, ReaderWriterLockSlim> lockers)
        {
            _lockers = lockers;
            _contextFactory = new ManagerCloudContextFactory();
        }

        internal void LoadItems(Tuple<Models.Client, Models.Item, Models.DataSource, Models.Sale> lineFileItems)
        {
            try
            {
                Expression<Func<Models.Client, bool>> clientSearchCriteria = x =>
                    x.FirstName == lineFileItems.Item1.FirstName && x.LastName == lineFileItems.Item1.LastName;
                Expression<Func<Models.Item, bool>> itemSearchCriteria = x => x.Name == lineFileItems.Item2.Name;
                Expression<Func<Models.DataSource, bool>> dataSourceSearchCriteria =
                    x => x.FileName == lineFileItems.Item3.FileName;
                Expression<Func<Models.Sale, bool>> saleSearchCriteria = x =>
                    x.Client.FirstName == lineFileItems.Item4.Client.FirstName &&
                    x.Client.LastName == lineFileItems.Item4.Client.LastName &&
                    x.DataSource.FileName == lineFileItems.Item4.DataSource.FileName &&
                    x.Item.Name == lineFileItems.Item4.Item.Name &&
                    x.Date == lineFileItems.Item4.Date && Math.Abs(x.SaleSum - lineFileItems.Item4.SaleSum) < 1e-6;

                using (var context = _contextFactory.CreateInstance())
                {
                    var unitOfWork = new UnitOfWork(context, _lockers);
                    unitOfWork.TryAddClient(lineFileItems.Item1, clientSearchCriteria);
                    unitOfWork.SaveChanges();
                    unitOfWork.TryAddItem(lineFileItems.Item2, itemSearchCriteria);
                    unitOfWork.SaveChanges();
                    unitOfWork.TryAddDataSource(lineFileItems.Item3, dataSourceSearchCriteria);
                    unitOfWork.SaveChanges();
                    unitOfWork.TryAddSale(lineFileItems.Item4, saleSearchCriteria);
                    unitOfWork.SaveChanges();
                }
               
                //AddSale(lineFileItems.Item4);
            }
            catch (ModelObjectException e)
            {
                LoggerHelper.AddErrorLog(new EventLog("LoadItemsError"), e.Message);
            }
        }

        //public DataSource AddDataSource(Models.DataSource newDataSource)
        //{
        //    DataSource addedDataSource;
        //    Expression<Func<Models.DataSource, bool>> dataSourceSearchCriteria =
        //        x => x.FileName == newDataSource.FileName;

        //    using (var context = _contextFactory.CreateInstance())
        //    {
        //        var dataSourceUnitOfWork = new DataSourceUnitOfWork(context, _repositoryFactory,
        //            ResolveLocker(typeof(Models.DataSource)));

        //        addedDataSource = dataSourceUnitOfWork.TryAdd(newDataSource, dataSourceSearchCriteria);
        //    }

        //    return addedDataSource;
        //}

        //public Client AddClient(Models.Client newClient)
        //{
        //    Client addedClient;
        //    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == newClient.FirstName && x.LastName == newClient.LastName;

        //    using (var context = _contextFactory.CreateInstance())
        //    {
        //        var clientUnitOfWork = new ClientUnitOfWork(context, _repositoryFactory,
        //            ResolveLocker(typeof(Models.Client)));

        //        addedClient = clientUnitOfWork.TryAdd(newClient, clientSearchCriteria);
        //    }

        //    return addedClient;
        //}

        //public Item AddItem(Models.Item newItem)
        //{
        //    Item addedItem;
        //    Expression<Func<Models.Item, bool>> itemSearchCriteria = x => x.Name == newItem.Name;

        //    using (var context = _contextFactory.CreateInstance())
        //    {
        //        var itemUnitOfWork = new ItemUnitOfWork(context, _repositoryFactory,
        //            ResolveLocker(typeof(Models.Item)));

        //        addedItem = itemUnitOfWork.TryAdd(newItem, itemSearchCriteria);
        //    }

        //    return addedItem;
        //}

        //public Sale AddSale(Models.Sale newSale)
        //{
        //    Sale addedSale;
        //    Expression<Func<Models.Sale, bool>> saleSearchCriteria = x => 
        //        x.Client.FirstName == newSale.Client.FirstName && x.Client.LastName == newSale.Client.LastName &&
        //        x.DataSource.FileName == newSale.DataSource.FileName && x.Item.Name == newSale.Item.Name &&
        //        x.Date == newSale.Date &&  Math.Abs(x.SaleSum - newSale.SaleSum) < 1e-6;

        //    using (var context = _contextFactory.CreateInstance())
        //    {
        //        var unitOfWork = new GenericUnitOfWork<Models.Sale, Sale>(context, _repositoryFactory,
        //            ResolveLocker(typeof(Models.Sale)));

        //        addedSale = unitOfWork.TryAdd(newSale, saleSearchCriteria);
        //    }

        //    return addedSale;
        //}
    }
}
