using System.Data.Entity;

namespace EF.ManagerCloud
{
    public class ManagerCloudContextFactory : IDbContextFactory
    {
        public DbContext CreateInstance()
        {
            return new ManagerCloudContextContainer();
        }
    }
}
