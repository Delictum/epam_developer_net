using System.Data.Entity;
using DAL.ManagerCloud.Contracts;

namespace DAL.ManagerCloud
{
    public class GenericRepositoryFactory : IRepositoryFactory
    {
        public IGenericRepository<T> CreateInstance<T>(DbContext context) where T : class
        {
            return new GenericRepository<T>(context);
        }
    }
}
