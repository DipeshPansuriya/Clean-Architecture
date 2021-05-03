using Application_Core.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application_Command
{
    public static class Application_Command_Startup
    {
        public static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            return services;
        }
    }
}