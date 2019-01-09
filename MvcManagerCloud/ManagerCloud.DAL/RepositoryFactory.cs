using ManagerCloud.DAL.Contracts;
using ManagerCloud.DAL.Repositories;
using ManagerCloud.EF;
using System.Data.Entity;

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
