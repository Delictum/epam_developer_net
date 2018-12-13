using System.Data.Entity;

namespace ManagerCloud.DAL.Contracts
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> CreateInstance<T>(DbContext context) where T : class;
    }
}
