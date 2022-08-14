using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Users_Database
{
    public static class Users_Database_Startup
    {
        public static void AddDatabase(this IServiceCollection services, string SQLConnstr)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<User_DbContext>(options =>
                options.UseSqlServer(SQLConnstr));
        }
    }
}