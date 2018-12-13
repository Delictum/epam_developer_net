using System.Data.Entity;

namespace EF.ManagerCloud
{
    public interface IDbContextFactory
    {
        DbContext CreateInstance();
    }
}
