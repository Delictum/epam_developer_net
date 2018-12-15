using ManagerCloud.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ManagerCloud.DAL.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        public ClientRepository()
        {
        }

        public ClientRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<Client>();
        }

        public override int GetId(Expression<Func<Client, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate).Id;
        }
    }
}
