using Application_Database;
using Application_Infrastructure;
using Application_Infrastructure.Startup_Proj;
using Microsoft.AspNetCore.Http.Extensions;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

StartupProj.AddStartupGetway(builder);
StartupProj.AddSerilog(builder);
StartupProj.AddSwagger(builder);
StartupProj.AddToken(builder);
StartupProj.AddHangfire(builder);
StartupProj.AddCORS(builder);

#region Project Dependancy

builder.Services.AddOcelot().AddConsul().AddCacheManager(x =>
{
    x.WithDictionaryHandle();
});
builder.Services.AddDatabase();
builder.Services.AddRepositories();
builder.Services.AddInfrastructure();

#endregion Project Dependancy

StartupProj.AddGenricAppBuilder(builder);

/// <summary>
/// Configure the HTTP request pipeline.
/// </summary>
WebApplication app = builder.Build();

app.UseHttpLogging();

StartupProj.AddException(app);

if (builder.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((context, next) =>
{
    string url = context.Request.GetDisplayUrl();
    return next.Invoke();
});

StartupProj.AddGenricApp(app);
StartupProj.AddMiddleware(app);

//var configuration = new OcelotPipelineConfiguration
//{
//    PreErrorResponderMiddleware = async (ctx, next) =>
//    {
//        await next.Invoke();
//    }
//};
//app.UseOcelot(configuration).Wait();
app.UseOcelot().Wait();

app.Run();