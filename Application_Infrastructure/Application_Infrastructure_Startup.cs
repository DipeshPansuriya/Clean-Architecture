using Application_Core;
using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Infrastructure.Background;
using Application_Infrastructure.Cache;
using Application_Infrastructure.Notificaion;
using Application_Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Infrastructure
{
    public static class Application_Infrastructure_Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICacheService, CacheService>();
            services.AddScoped<CacheService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IBackgroundJob, BackgroundJob>();
            services.AddTransient<INotificationMsg, NotificationMsg>();
            services.AddScoped<NotificationMsg>();
            services.AddTransient(typeof(IDapper<>), typeof(Dapper<>));
            services.AddScoped<IResponse_Request, Response_Request>();

            return services;
        }
    }
}