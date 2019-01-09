using ManagerCloud.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ManagerCloud.DAL.Repositories
{
    public class ItemRepository : BaseRepository<Item>
    {
        public ItemRepository()
        {
        }

        public ItemRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<Item>();
        }

        public override int GetId(Expression<Func<Item, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate).Id;
        }
    }
}
