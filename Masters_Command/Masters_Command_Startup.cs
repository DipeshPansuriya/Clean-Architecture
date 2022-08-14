using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Masters_Command
{
    public static class Masters_Command_Startup
    {
        public static IServiceCollection AddMastersCommand(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            return services;
        }
    }
}