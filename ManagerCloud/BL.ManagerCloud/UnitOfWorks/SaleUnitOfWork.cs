using BL.ManagerCloud.Models;
using DAL.ManagerCloud.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace BL.ManagerCloud.UnitOfWorks
{
    public class SaleUnitOfWork : GenericUnitOfWork<Sale, EF.ManagerCloud.Sale>
    {
        public SaleUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<Sale, EF.ManagerCloud.Sale> ToEntity
        {
            get
            {
                return x => new EF.ManagerCloud.Sale()
                {
                    Id = x.SaleId,
                    Client = new EF.ManagerCloud.Client
                    {
                        Id = x.Client.Id,
                        FirstName = x.Client.FirstName,
                        LastName = x.Client.LastName
                    },
                    Item = new EF.ManagerCloud.Item
                    {
                        Id = x.Item.Id,
                        Name = x.Item.Name
                    },
                    Date = x.Date,
                    SaleSum = x.SaleSum,
                    DataSource = new EF.ManagerCloud.DataSource
                    {
                        Id = x.DataSource.Id,
                        FileName = x.DataSource.FileName
                    }

                };
            }
        }

        protected override Func<EF.ManagerCloud.Sale, Sale> ToModel
        {
            get
            {
                return x => new Sale()
                {
                    SaleId = x.Id,
                    Client = new Client
                    {
                        Id = x.Client.Id,
                        FirstName = x.Client.FirstName,
                        LastName = x.Client.LastName
                    },
                    Item = new Item
                    {
                        Id = x.Item.Id,
                        Name = x.Item.Name
                    },
                    Date = x.Date,
                    SaleSum = x.SaleSum,
                    DataSource = new DataSource
                    {
                            Id = x.DataSource.Id,
                            FileName = x.DataSource.FileName
                    }
                };
            }
        }

        public new EF.ManagerCloud.Sale TryAdd(Sale sale, Expression<Func<Sale, bool>> searchExpression, bool requiredBlocking)
        {
            EF.ManagerCloud.Sale newSale;
            if (requiredBlocking) _locker.EnterWriteLock();
            try
            {
                var getSale = TryGet(searchExpression);
                if (getSale != null)
                {
                    newSale = ToEntity(getSale);
                    return newSale;
                }

                var saleRepository = _repoFactory.CreateInstance<EF.ManagerCloud.Sale>(_context);
                newSale = ToEntity(sale);

                DatabaseItemLoader.DisableAddingSecondaryEntitiesFromSale(newSale);
                DatabaseItemLoader.EnableAddingForeignKeysFromSale(newSale, new []{ sale.Client.Id, sale.Item.Id, sale.DataSource.Id });

                saleRepository.Add(newSale);
                saleRepository.Save();
            }
            finally
            {
                if (requiredBlocking) _locker.ExitWriteLock();
            }

            return newSale;
        }
    }
}
