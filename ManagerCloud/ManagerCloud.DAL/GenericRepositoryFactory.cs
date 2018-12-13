using ManagerCloud.DAL.Contracts;
using System.Data.Entity;

namespace ManagerCloud.DAL
{
    public class GenericRepositoryFactory : IRepositoryFactory
    {
        public IGenericRepository<T> CreateInstance<T>(DbContext context) where T : class
        {
            return new GenericRepository<T>(context);
        }
    }
}
