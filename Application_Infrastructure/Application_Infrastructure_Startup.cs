using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Notification;
using Application_Infrastructure.Background;
using Application_Infrastructure.Cache;
using Application_Infrastructure.Notificaion;
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
            services.AddSingleton<IBackgroundJob, BackgroundJob>();

            services.AddTransient<INotificationMsg, NotificationMsg>();
            services.AddScoped<NotificationMsg>();

            return services;
        }
    }
}