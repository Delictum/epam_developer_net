using ManagerCloud.DAL.Contracts;
using System.Data.Entity;
using ManagerCloud.DAL.Repositories;
using ManagerCloud.EF;

namespace ManagerCloud.DAL
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<Client> CreateInstanceClient(DbContext context)
        {
            return new ClientRepository(context);
        }

        public IRepository<Item> CreateInstanceItem(DbContext context)
        {
            return new ItemRepository(context);
        }

        public IRepository<DataSource> CreateInstanceDataSource(DbContext context)
        {
            return new DataSourceRepository(context);
        }

        public IRepository<Sale> CreateInstanceSale(DbContext context)
        {
            return new SaleRepository(context);
        }
    }
}
