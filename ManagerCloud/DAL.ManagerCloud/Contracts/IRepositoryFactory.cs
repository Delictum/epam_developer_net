using System.Data.Entity;

namespace DAL.ManagerCloud.Contracts
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> CreateInstance<T>(DbContext context) where T : class;
    }
}
