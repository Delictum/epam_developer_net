using System.Data.Entity;

namespace ManagerCloud.EF
{
    public interface IDbContextFactory
    {
        DbContext CreateInstance();
    }
}
