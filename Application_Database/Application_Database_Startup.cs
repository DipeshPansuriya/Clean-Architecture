using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Database
{
    public static class Application_Database_Startup
    {
        public static void AddDatabase(this IServiceCollection services, string SQLConnstr)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<APP_DbContext>(options =>
                options.UseSqlServer(SQLConnstr));
        }
    }
}