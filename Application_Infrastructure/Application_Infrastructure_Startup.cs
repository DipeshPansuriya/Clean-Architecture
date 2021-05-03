using Application_Core.Interfaces;
using Application_Infrastructure.BackgroundTask;
using Application_Infrastructure.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Infrastructure
{
    public static class Application_Infrastructure_Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICacheService, CacheService>();
            services.AddScoped<CacheService>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IQueueService, QueueService>();
            services.AddSingleton<IBackgroundJob, BackgroundJob>();
            return services;
        }
    }
}