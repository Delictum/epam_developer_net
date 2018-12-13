using ManagerCloud.BL.Models;
using ManagerCloud.DAL.Contracts;
using System;
using System.Data.Entity;
using System.Threading;

namespace ManagerCloud.BL.UnitOfWorks
{
    public class ClientUnitOfWork : GenericUnitOfWork<Client, EF.Client>
    {
        public ClientUnitOfWork(DbContext context, IRepositoryFactory repoFactory, ReaderWriterLockSlim locker)
            : base(context, repoFactory, locker)
        {
        }

        protected override Func<Client, EF.Client> ToEntity
        {
            get { return x => new EF.Client() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }; }
        }

        protected override Func<EF.Client, Client> ToModel
        {
            get { return x => new Client() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }; }
        }
    }
}
