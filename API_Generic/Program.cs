using Application_Common;
using Application_Database;
using Application_Infrastructure;
using Application_Infrastructure.Startup_Proj;
using Generic_Command;
using Generic_Command.InsertUpdate.Menu_InstUpd;
using Generic_Command.InsertUpdate.Prod_InstUpd;
using MediatR;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

StartupProj.AddStartup(builder);
StartupProj.AddSerilog(builder);
StartupProj.AddSwagger(builder);
StartupProj.AddToken(builder);
StartupProj.AddHangfire(builder);
StartupProj.AddCORS(builder);

#region Project Dependancy

builder.Services.AddGenericCommand();
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

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    IMediator mediator = services.GetRequiredService<IMediator>();
    await mediator.Send(new Menu_InstUpd { }, CancellationToken.None);
    await mediator.Send(new Prod_InstUpd { }, CancellationToken.None);
}

app.Run();