using Microsoft.EntityFrameworkCore;

namespace Users_Database
{
    public class UserDbContextFactory : DesignTimeUserDbBase<User_DbContext>
    {
        protected override User_DbContext CreateNewInstance(DbContextOptions<User_DbContext> options)
        {
            return new User_DbContext(options);
        }
    }
}