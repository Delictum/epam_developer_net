using ManagerCloud.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ManagerCloud.DAL.Repositories
{
    public class DataSourceRepository : BaseRepository<DataSource>
    {
        public DataSourceRepository()
        {
        }

        public DataSourceRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<DataSource>();
        }

        public override int GetId(Expression<Func<DataSource, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate).Id;
        }
    }
}
