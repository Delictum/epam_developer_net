using ManagerCloud.Core.CustomExceptions.ModelObjectException;
using ManagerCloud.Core.Helpers;
using ManagerCloud.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using Client = ManagerCloud.BL.Models.Client;
using DataSource = ManagerCloud.BL.Models.DataSource;
using Item = ManagerCloud.BL.Models.Item;
using Sale = ManagerCloud.BL.Models.Sale;

namespace ManagerCloud.BL
{
    internal class DatabaseItemLoader
    {
        private readonly IDictionary<Type, ReaderWriterLockSlim> _lockers;
        private readonly DbContext _dbContext;

        public DatabaseItemLoader(IDictionary<Type, ReaderWriterLockSlim> lockers, DbContext dbContext)
        {
            _lockers = lockers;
            _dbContext = dbContext;
        }

        internal void LoadItems(Tuple<Client, Item, DataSource, Sale> lineFileItems)
        {
            var searchCriteria = GetEachSearchCriteria(lineFileItems);
            try
            {
                var unitOfWork = new UnitOfWork(_dbContext, _lockers);

                unitOfWork.TryAddClient(lineFileItems.Item1, searchCriteria.Item1);
                unitOfWork.TryAddItem(lineFileItems.Item2, searchCriteria.Item2);
                unitOfWork.TryAddDataSource(lineFileItems.Item3, searchCriteria.Item3);
                unitOfWork.TryAddSale(lineFileItems.Item4, searchCriteria.Item4);
            }
            catch (ModelObjectException e)
            {
                LoggerHelper.AddErrorLog(new EventLog("LoadItemsError"), e.Message);
            }
        }

        private Tuple<Expression<Func<Client, bool>>, Expression<Func<Item, bool>>,
            Expression<Func<DataSource, bool>>, Expression<Func<Sale, bool>>> GetEachSearchCriteria(
            Tuple<Client, Item, DataSource, Sale> lineFileItems)
        {
            Expression<Func<Client, bool>> clientSearchCriteria = x =>
                x.FirstName == lineFileItems.Item1.FirstName && x.LastName == lineFileItems.Item1.LastName;
            Expression<Func<Item, bool>> itemSearchCriteria = x => x.Name == lineFileItems.Item2.Name;
            Expression<Func<DataSource, bool>> dataSourceSearchCriteria =
                x => x.FileName == lineFileItems.Item3.FileName;
            Expression<Func<Sale, bool>> saleSearchCriteria = x =>
                x.Client.FirstName == lineFileItems.Item4.Client.FirstName &&
                x.Client.LastName == lineFileItems.Item4.Client.LastName &&
                x.DataSource.FileName == lineFileItems.Item4.DataSource.FileName &&
                x.Item.Name == lineFileItems.Item4.Item.Name &&
                x.Date == lineFileItems.Item4.Date && Math.Abs(x.SaleSum - lineFileItems.Item4.SaleSum) < 1e-6;
            return new
                Tuple<Expression<Func<Client, bool>>, Expression<Func<Item, bool>>, Expression<Func<DataSource, bool>>,
                    Expression<Func<Sale, bool>>>(clientSearchCriteria, itemSearchCriteria, dataSourceSearchCriteria,
                    saleSearchCriteria);
        }
    }
}
