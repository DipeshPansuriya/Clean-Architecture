using Microsoft.EntityFrameworkCore;

namespace Users_Database
{
    public class LogDbContextFactory : DesignTimeLogDbContextFactoryBase<APP_Log_DbContext>
    {
        protected override APP_Log_DbContext CreateNewInstance(DbContextOptions<APP_Log_DbContext> options)
        {
            return new APP_Log_DbContext(options);
        }
    }
}