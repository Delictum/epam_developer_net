using System.Data.Entity;

namespace ManagerCloud.EF
{
    public class ManagerCloudContextFactory : IDbContextFactory
    {
        public DbContext CreateInstance()
        {
            return new ManagerCloudContextContainer();
        }
    }
}
