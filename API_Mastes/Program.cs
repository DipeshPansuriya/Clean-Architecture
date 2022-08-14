using Application_Common;
using Application_Database;
using Application_Infrastructure;
using Application_Infrastructure.Startup_Proj;
using Masters_Command;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

StartupProj.AddStartup(builder);
StartupProj.AddSerilog(builder);
StartupProj.AddSwagger(builder);
StartupProj.AddToken(builder);
StartupProj.AddHangfire(builder);
StartupProj.AddCORS(builder);

#region Project Dependancy

builder.Services.AddMastersCommand();
builder.Services.AddDatabase(APISetting.DBConnection);
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

StartupProj.AddGenricApp(app);
StartupProj.AddMiddleware(app);

app.Run();