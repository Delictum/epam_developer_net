using System;
using System.Data.Entity;
using System.Threading;
using BL.ManagerCloud.Models;
using DAL.ManagerCloud.Contracts;

namespace BL.ManagerCloud.UnitOfWorks
{
    public class DataSourceUnitOfWork : GenericUnitOfWork<DataSource, EF.ManagerCloud.DataSource>
    {
        public DataSourceUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<DataSource, EF.ManagerCloud.DataSource> ToEntity
        {
            get { return x => new EF.ManagerCloud.DataSource() { Id = x.Id, FileName = x.FileName }; }
        }

        protected override Func<EF.ManagerCloud.DataSource, DataSource> ToModel
        {
            get { return x => new Models.DataSource() { Id = x.Id, FileName = x.FileName }; }
        }
    }
}
