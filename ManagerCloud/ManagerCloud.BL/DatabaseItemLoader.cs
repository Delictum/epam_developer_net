using ManagerCloud.BL.UnitOfWorks;
using ManagerCloud.DAL;
using ManagerCloud.DAL.Contracts;
using ManagerCloud.EF;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace ManagerCloud.BL
{
    internal class DatabaseItemLoader
    {
        private readonly IDictionary<Type, ReaderWriterLockSlim> _lockers;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IDbContextFactory _contextFactory;

        public DatabaseItemLoader(Dictionary<Type, ReaderWriterLockSlim> lockers)
        {
            _lockers = lockers;
            _contextFactory = new ManagerCloudContextFactory();
            _repositoryFactory = new GenericRepositoryFactory();
        }

        private ReaderWriterLockSlim ResolveLocker(Type modelType)
        {
            return _lockers[modelType];
        }

        private static void ChangeSale(Models.Sale sale, int clientId, int itemId, int dataSourceId)
        {
            sale.Client.Id = clientId;
            sale.Item.Id = itemId;
            sale.DataSource.Id = dataSourceId;
        }

        public static void DisableAddingSecondaryEntitiesFromSale(Sale sale)
        {
            sale.Client = null;
            sale.Item = null;
            sale.DataSource = null;
        }

        public static void EnableAddingForeignKeysFromSale(Sale sale, IReadOnlyList<int> foreignKeys)
        {
            sale.ClientId = foreignKeys[0];
            sale.ItemId = foreignKeys[1];
            sale.DataSourceId = foreignKeys[2];
        }

        internal void LoadItems(Tuple<Models.Client, Models.Item, Models.DataSource, Models.Sale> lineFileItems)
        {
            var client = AddClient(lineFileItems.Item1);
            var item = AddItem(lineFileItems.Item2);
            var dataSource = AddDataSource(lineFileItems.Item3);

            ChangeSale(lineFileItems.Item4, client.Id, item.Id, dataSource.Id);

            AddSale(lineFileItems.Item4);
        }

        public DataSource AddDataSource(Models.DataSource newDataSource)
        {
            DataSource addedDataSource;
            Expression<Func<Models.DataSource, bool>> dataSourceSearchCriteria =
                x => x.FileName == newDataSource.FileName;

            using (var context = _contextFactory.CreateInstance())
            {
                var dataSourceUnitOfWork = new DataSourceUnitOfWork(context, _repositoryFactory,
                    ResolveLocker(typeof(Models.DataSource)));

                addedDataSource = dataSourceUnitOfWork.TryAdd(newDataSource, dataSourceSearchCriteria);
            }

            return addedDataSource;
        }

        public Client AddClient(Models.Client newClient)
        {
            Client addedClient;
            Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == newClient.FirstName && x.LastName == newClient.LastName;

            using (var context = _contextFactory.CreateInstance())
            {
                var clientUnitOfWork = new ClientUnitOfWork(context, _repositoryFactory,
                    ResolveLocker(typeof(Models.Client)));

                addedClient = clientUnitOfWork.TryAdd(newClient, clientSearchCriteria);
            }

            return addedClient;
        }

        public Item AddItem(Models.Item newItem)
        {
            Item addedItem;
            Expression<Func<Models.Item, bool>> itemSearchCriteria = x => x.Name == newItem.Name;

            using (var context = _contextFactory.CreateInstance())
            {
                var itemUnitOfWork = new ItemUnitOfWork(context, _repositoryFactory,
                    ResolveLocker(typeof(Models.Item)));

                addedItem = itemUnitOfWork.TryAdd(newItem, itemSearchCriteria);
            }

            return addedItem;
        }

        public Sale AddSale(Models.Sale newSale)
        {
            Sale addedSale;
            Expression<Func<Models.Sale, bool>> saleSearchCriteria = x => 
                x.Client.FirstName == newSale.Client.FirstName && x.Client.LastName == newSale.Client.LastName &&
                x.DataSource.FileName == newSale.DataSource.FileName && x.Item.Name == newSale.Item.Name &&
                x.Date == newSale.Date &&  Math.Abs(x.SaleSum - newSale.SaleSum) < 1e-6;

            using (var context = _contextFactory.CreateInstance())
            {
                var saleUnitOfWork = new SaleUnitOfWork(context, _repositoryFactory,
                    ResolveLocker(typeof(Models.Sale)));

                addedSale = saleUnitOfWork.TryAdd(newSale, saleSearchCriteria);
            }

            return addedSale;
        }
    }
}
