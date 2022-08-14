using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Login_Command
{
    public static class Login_Command_Startup
    {
        public static IServiceCollection AddLoginCommand(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            return services;
        }
    }
}