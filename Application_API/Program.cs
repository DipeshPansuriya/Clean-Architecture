using Application_API.JobScheduler;
using Application_Command;
using Application_Database;
using Application_Genric;
using Application_Infrastructure;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Email;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

#region SetAPPSetting

APISetting config = new APISetting();
builder.Configuration.Bind("AppSettings", config);

APISetting.XMLFilePath = builder.Environment.WebRootPath + @"\XMLQuery\";

#endregion SetAPPSetting

#region SetSerilog

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
#pragma warning disable CS0618 // Type or member is obsolete
loggerConfig
.WriteTo.MSSqlServer(
    connectionString: APISetting.DBConnection,
    tableName: "Logs",
    schemaName: "dbo",
    autoCreateSqlTable: true,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
    )
#pragma warning restore CS0618 // Type or member is obsolete

.WriteTo.File(
    path: builder.Environment.ContentRootPath + @"Logs\Error\",
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
    outputTemplate: "{NewLine}{Timestamp:o} {NewLine}" +
                    "--------------------SourceContext [{Level}]--------------------{NewLine}" +
                    "({SourceContext}) {NewLine}" +
                    "--------------------Message--------------------{NewLine}" +
                    "{Message}{NewLine}" +
                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX{NewLine}",
    rollingInterval: RollingInterval.Day
    )
.WriteTo.Email(
    new EmailConnectionInfo
    {
        FromEmail = "howard0@ethereal.email",
        ToEmail = "dipeshpansuriya@ymail.com",
        MailServer = APISetting.EmailConfiguration.SMTPAddress,
        NetworkCredentials = new NetworkCredential
        {
            UserName = APISetting.EmailConfiguration.UserId,
            Password = APISetting.EmailConfiguration.Password,
        },
        EnableSsl = APISetting.EmailConfiguration.SSL,
        IsBodyHtml = false,
        Port = APISetting.EmailConfiguration.Port,
        EmailSubject = "[{Level}] <{MachineName}> Log Email",
    },
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm} [{Level}] <{MachineName}> {Message}{NewLine}{Exception}",
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
    mailSubject: "[{Level}] <{MachineName}> Log Email"
    )
.WriteTo.Console(
    outputTemplate: "{NewLine}{Timestamp:o} {NewLine}" +
                    "--------------------SourceContext [{Level}]--------------------{NewLine}" +
                    "({SourceContext}) {NewLine}" +
                    "--------------------Message--------------------{NewLine}" +
                    "{Message}{NewLine}" +
                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX{NewLine}"
                    )
);

#endregion SetSerilog

/// <summary>
/// Add services to the container.
/// </summary>

#region Project Dependancy

builder.Services.AddDatabase();
builder.Services.AddCommand();
builder.Services.AddRepositories();
builder.Services.AddInfrastructure();

#endregion Project Dependancy

// Add Cookie Policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed
    // for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

#region Kestrel & IIS Server

// If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

#endregion Kestrel & IIS Server

builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression();
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddControllers();

// Hangfire
builder.Services.AddHangfire(config =>
       config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseDefaultTypeSerializer()
       .UseSqlServerStorage(APISetting.DBConnection));

builder.Services.AddHangfireServer();

#region Caching

// In-Memory Caching
builder.Services.AddMemoryCache();

// Redis Caching
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = APISetting.CacheConfiguration.CacheURL;
});

#endregion Caching

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application_API", Version = "v1" });
    c.CustomSchemaIds(x => x.FullName);
});

// Customize default API behavior Start
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Cors
if (!string.IsNullOrEmpty(APISetting.CORSAllowOrigin))
{
    // Allowed specified domain to access the API
    builder.Services.AddCors(options =>
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
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                //.AllowCredentials()
                .WithExposedHeaders("X-Pagination"));
    });
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/// <summary>
/// Configure the HTTP request pipeline.
/// </summary>
WebApplication app = builder.Build();

app.UseHttpLogging();

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

if (builder.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application_API v1"));
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// Cors
app.UseCors("CorsPolicy");

// Hangfire
app.UseHangfireDashboard();

// Recurring Job for every 5 min
RecuringJob recuringJob = new RecuringJob();
#pragma warning disable CS0612 // Type or member is obsolete
recuringJob.Job();
#pragma warning restore CS0612 // Type or member is obsolete

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard("/hangfire");
});

app.Run();

/// <summary>
/// RecuringJob
/// </summary>
public class RecuringJob
{
    private static readonly IMediator _mediator;
    private readonly EmailScheduler jobscheduler = new EmailScheduler(_mediator);
    //private readonly IRecurringJobManager recurringJobManager;

    [System.Obsolete]
    public void Job()
    {
        RecurringJobManager recurringJobManager = new RecurringJobManager();
        recurringJobManager.AddOrUpdate("Send Pending Mail : Runs Every 15 Min",
            () => jobscheduler.SendPendingMail(), Cron.MinuteInterval(15)
            );
    }
}