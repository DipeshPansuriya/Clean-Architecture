using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;
using System.Reflection;

namespace Application_API
{
    public class Program
    {
        //private static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope())
            {
                System.IServiceProvider services = scope.ServiceProvider;
                ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
                Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("app");
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom
                    .Configuration(hostingContext.Configuration)
                        .WriteTo.Email(new EmailConnectionInfo
                        {
                            FromEmail = "howard0@ethereal.email",
                            ToEmail = "dipeshpansuriya@ymail.com",
                            MailServer = "smtp.ethereal.email",
                            NetworkCredentials = new NetworkCredential
                            {
                                UserName = "howard0@ethereal.email",
                                Password = "Ays797tvgxZSptbSHd"
                            },
                            EnableSsl = false,
                            IsBodyHtml = false,
                            Port = 587,
                            EmailSubject = "[{Level}] <{MachineName}> Log Email",
                        },
                         outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm} [{Level}] <{MachineName}> {Message}{NewLine}{Exception}",
                         restrictedToMinimumLevel: LogEventLevel.Error
                         //, batchPostingLimit: 1
                         )
                    )

                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    IWebHostEnvironment env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        Assembly appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .UseStartup<Startup>()
               ;
        }
    }
}