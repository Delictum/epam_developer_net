using System;
using System.Data.Entity;
using System.Threading;
using ManagerCloud.BL.Models;
using ManagerCloud.DAL.Contracts;

namespace ManagerCloud.BL.UnitOfWorks
{
    public class DataSourceUnitOfWork : GenericUnitOfWork<DataSource, EF.DataSource>
    {
        public DataSourceUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<DataSource, EF.DataSource> ToEntity
        {
            get { return x => new EF.DataSource() { Id = x.Id, FileName = x.FileName }; }
        }

        protected override Func<EF.DataSource, DataSource> ToModel
        {
            get { return x => new Models.DataSource() { Id = x.Id, FileName = x.FileName }; }
        }
    }
}
