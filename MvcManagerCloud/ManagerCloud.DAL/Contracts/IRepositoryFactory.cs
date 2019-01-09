using System.Data.Entity;
using ManagerCloud.EF;

namespace ManagerCloud.DAL.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<Client> CreateInstanceClient(DbContext context);
        IRepository<Item> CreateInstanceItem(DbContext context);
        IRepository<DataSource> CreateInstanceDataSource(DbContext context);
        IRepository<Sale> CreateInstanceSale(DbContext context);
    }
}
