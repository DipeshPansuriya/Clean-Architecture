using Application_Core.Background;
using Application_Infrastructure.Background;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Infrastructure
{
    public static class Application_Background_Startup
    {
        public static IServiceCollection AddBackground(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundJob, BackgroundJob>();

            return services;
        }
    }
}