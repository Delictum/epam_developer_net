using ManagerCloud.BL.Models;
using ManagerCloud.DAL.Contracts;
using System;
using System.Data.Entity;
using System.Threading;

namespace ManagerCloud.BL.UnitOfWorks
{
    public class ItemUnitOfWork : GenericUnitOfWork<Item, EF.Item>
    {
        public ItemUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<Item, EF.Item> ToEntity
        {
            get { return x => new EF.Item() { Id = x.Id, Name = x.Name }; }
        }

        protected override Func<EF.Item, Item> ToModel
        {
            get { return x => new Item() { Id = x.Id, Name = x.Name }; }
        }
    }
}
