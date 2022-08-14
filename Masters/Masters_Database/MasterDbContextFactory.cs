using Microsoft.EntityFrameworkCore;

namespace Masters_Database
{
    public class MasterDbContextFactory : DesignTimeMasterDbBase<Master_DbContext>
    {
        protected override Master_DbContext CreateNewInstance(DbContextOptions<Master_DbContext> options)
        {
            return new Master_DbContext(options);
        }
    }
}