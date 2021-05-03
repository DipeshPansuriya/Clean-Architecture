using Application_Core.Interfaces;
using Application_Database.Repositories;
using Application_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application_Database
{
    public static class Application_Database_Startup
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<APP_DbContext>(options =>
                options.UseSqlServer(APISetting.DBConnection));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            //services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDapper, Dapperr>();
            services.AddTransient<IGetQuery, GetQuery>();

            #endregion Repositories
        }
    }
}