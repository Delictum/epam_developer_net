using BL.ManagerCloud.Models;
using DAL.ManagerCloud.Contracts;
using System;
using System.Data.Entity;
using System.Threading;

namespace BL.ManagerCloud.UnitOfWorks
{
    public class ClientUnitOfWork : GenericUnitOfWork<Client, EF.ManagerCloud.Client>
    {
        public ClientUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<Client, EF.ManagerCloud.Client> ToEntity
        {
            get { return x => new EF.ManagerCloud.Client() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }; }
        }

        protected override Func<EF.ManagerCloud.Client, Client> ToModel
        {
            get { return x => new Client() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }; }
        }
    }
}
