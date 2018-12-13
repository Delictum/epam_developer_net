using ManagerCloud.BL.Models;
using ManagerCloud.DAL.Contracts;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;

namespace ManagerCloud.BL.UnitOfWorks
{
    public class SaleUnitOfWork : GenericUnitOfWork<Sale, EF.Sale>
    {
        public SaleUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<Sale, EF.Sale> ToEntity
        {
            get
            {
                return x => new EF.Sale()
                {
                    Id = x.SaleId,
                    Client = new EF.Client
                    {
                        Id = x.Client.Id,
                        FirstName = x.Client.FirstName,
                        LastName = x.Client.LastName
                    },
                    Item = new EF.Item
                    {
                        Id = x.Item.Id,
                        Name = x.Item.Name
                    },
                    Date = x.Date,
                    SaleSum = x.SaleSum,
                    DataSource = new EF.DataSource
                    {
                        Id = x.DataSource.Id,
                        FileName = x.DataSource.FileName
                    }

                };
            }
        }

        protected override Func<EF.Sale, Sale> ToModel
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

        public new EF.Sale TryAdd(Sale sale, Expression<Func<Sale, bool>> searchExpression)
        {
            EF.Sale newSale;
            _locker.EnterWriteLock();
            try
            {
                var getSale = TryGet(searchExpression);
                if (getSale != null)
                {
                    newSale = ToEntity(getSale);
                    return newSale;
                }

                var saleRepository = _repoFactory.CreateInstance<EF.Sale>(_context);
                newSale = ToEntity(sale);

                DatabaseItemLoader.DisableAddingSecondaryEntitiesFromSale(newSale);
                DatabaseItemLoader.EnableAddingForeignKeysFromSale(newSale, new []{ sale.Client.Id, sale.Item.Id, sale.DataSource.Id });

                saleRepository.Add(newSale);
                _context.SaveChanges();
            }
            finally
            {
                _locker.ExitWriteLock();
            }

            return newSale;
        }
    }
}
