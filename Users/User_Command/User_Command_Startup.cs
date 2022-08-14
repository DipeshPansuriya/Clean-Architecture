using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace User_Command
{
    public static class User_Command_Startup
    {
        public static IServiceCollection AddUserCommand(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            return services;
        }
    }
}