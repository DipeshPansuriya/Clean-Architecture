using Application_API.JobScheduler;
using Application_Command;
using Application_Database;
using Application_Genric;
using Application_Infrastructure;
using Hangfire;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        private static readonly IMediator _mediator;
        private readonly EmailScheduler jobscheduler = new EmailScheduler(_mediator);

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetAppSetting();
            //services.AddMediatR(typeof(Demo_Customer_Inst_cmd).GetTypeInfo().Assembly);

            // Project Dependancy
            services.AddDatabase(Configuration);
            services.AddCommand();
            services.AddRepositories();
            services.AddInfrastructure();

            // Add Health Check
            services.AddHealthChecks()
                 //.AddUrlGroup(new Uri("https://localhost:5001/weatherforecast"), name: "base URL", failureStatus: HealthStatus.Degraded)
                 .AddSqlServer(APISetting.DBConnection,
                 healthQuery: "select 1",
                 failureStatus: HealthStatus.Degraded,
                 name: "SQL Server");

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
                opt.AddHealthCheckEndpoint("default api", "/health"); //map health check api
            })
            .AddInMemoryStorage();

            // Add Cookie Policy
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed
                // for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddHttpContextAccessor();
            services.AddResponseCaching();
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddControllers();

            // Hangfire
            services.AddHangfire(config =>
                   config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseDefaultTypeSerializer()
                   .UseSqlServerStorage(APISetting.DBConnection));

            services.AddHangfireServer();

            // In-Memory Caching
            services.AddMemoryCache();

            // Redis Caching
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            // SignalR
            //services.AddSignalR();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application_API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
            });

            // Customize default API behavior Start
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Cors
            if (!string.IsNullOrEmpty(APISetting.CORSAllowOrigin))
            {
                // Allowed specified domain to access the API
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.WithOrigins(APISetting.CORSAllowOrigin)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            //.AllowCredentials()
                            .WithExposedHeaders("X-Pagination"));
                });
            }
            else
            {
                // Allowed all domain to access the API
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            // .AllowCredentials()
                            .WithExposedHeaders("X-Pagination"));
                });
            }
        }

        private void SetAppSetting()
        {
            APISetting config = new APISetting();

            Configuration.Bind("AppSettings", config);
            APISetting.XMLFilePath = Environment.WebRootPath + @"\XMLQuery\";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = (c) =>
                {
                    IExceptionHandlerFeature exception = c.Features.Get<IExceptionHandlerFeature>();
                    HttpStatusCode statusCode = exception.Error.GetType().Name switch
                    {
                        "ArgumentException" => HttpStatusCode.BadRequest,
                        _ => HttpStatusCode.ServiceUnavailable
                    };

                    c.Response.StatusCode = (int)statusCode;
                    byte[] content = Encoding.UTF8.GetBytes($"Error [{exception.Error.Message}]");
                    c.Response.Body.WriteAsync(content, 0, content.Length);

                    return Task.CompletedTask;
                }
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application_API v1"));
            }

            // HealthCheck UI /healthchecks-ui
            app.UseHealthChecksUI();

            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Cors
            app.UseCors("CorsPolicy");

            // global error handler
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            //app.ConfigureExceptionHandler();

            // Hangfire
            app.UseHangfireDashboard();

            // Recurring Job for every 5 min
            recurringJobManager.AddOrUpdate("Send Pending Mail : Runs Every 15 Min",
                () => jobscheduler.SendPendingMail(), Cron.MinuteInterval(15)
                );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard("/hangfire");
                endpoints.MapHealthChecks("/health",
                   new HealthCheckOptions()
                   {
                       Predicate = _ => true,
                       ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                   });
            });
        }
    }
}