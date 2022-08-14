using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Masters_Database
{
    public static class Master_Database_Startup
    {
        public static void AddDatabase(this IServiceCollection services, string SQLConnstr)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<Master_DbContext>(options =>
                options.UseSqlServer(SQLConnstr));
        }
    }
}