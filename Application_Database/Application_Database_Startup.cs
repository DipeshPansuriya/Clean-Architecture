using Application_Core.Repositories;
using Application_Database.Repositories;
using Application_Genric;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Database
{
    public static class Application_Database_Startup
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<APP_DbContext>(options =>
                options.UseSqlServer(APISetting.DBConnection));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            //services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            //services.AddTransient<IDapper, Dapperr>();
            services.AddTransient(typeof(IDapper<>), typeof(Dapper<>));
            services.AddTransient<IGetQuery, GetQuery>();

            #endregion Repositories
        }
    }
}