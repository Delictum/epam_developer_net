using BL.ManagerCloud.Models;
using DAL.ManagerCloud.Contracts;
using System;
using System.Data.Entity;
using System.Threading;

namespace BL.ManagerCloud.UnitOfWorks
{
    public class ItemUnitOfWork : GenericUnitOfWork<Item, EF.ManagerCloud.Item>
    {
        public ItemUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<Item, EF.ManagerCloud.Item> ToEntity
        {
            get { return x => new EF.ManagerCloud.Item() { Id = x.Id, Name = x.Name }; }
        }

        protected override Func<EF.ManagerCloud.Item, Item> ToModel
        {
            get { return x => new Item() { Id = x.Id, Name = x.Name }; }
        }
    }
}
