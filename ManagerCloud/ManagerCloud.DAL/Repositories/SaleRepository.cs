using ManagerCloud.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ManagerCloud.DAL.Repositories
{
    public class SaleRepository : BaseRepository<Sale>
    {
        public SaleRepository()
        {
        }

        public SaleRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<Sale>();
        }
        
        public override int GetId(Expression<Func<Sale, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate).Id;
        }
    }
}
