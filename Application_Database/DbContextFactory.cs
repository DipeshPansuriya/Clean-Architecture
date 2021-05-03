using Microsoft.EntityFrameworkCore;

namespace Application_Database
{
    public class DbContextFactory : DesignTimeDbContextFactoryBase<APP_DbContext>
    {
        protected override APP_DbContext CreateNewInstance(DbContextOptions<APP_DbContext> options)
        {
            return new APP_DbContext(options);
        }
    }
}