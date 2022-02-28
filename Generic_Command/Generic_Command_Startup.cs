using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Generic_Command
{
    public static class Generic_Command_Startup
    {
        public static IServiceCollection AddGenericCommand(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            return services;
        }
    }
}