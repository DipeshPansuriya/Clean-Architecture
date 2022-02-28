using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Registrations_Command
{
    public static class Registrations_Command_Startup
    {
        public static IServiceCollection AddRegistrationsCommand(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            return services;
        }
    }
}