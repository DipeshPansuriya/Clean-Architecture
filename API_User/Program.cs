using Application_Common;
using Application_Infrastructure;
using Application_Infrastructure.Startup_Proj;
using MediatR;
using User_Command;
using User_Command.AdminOrg.InsertUpdate;
using User_Command.InsertUpdate.Menu_InstUpd;
using User_Command.InsertUpdate.Prod_InstUpd;
using Users_Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

StartupProj.AddStartup(builder);
StartupProj.AddSerilog(builder);
StartupProj.AddSwagger(builder);
StartupProj.AddToken(builder);
StartupProj.AddHangfire(builder);
StartupProj.AddCORS(builder);

#region Project Dependancy

builder.Services.AddUserCommand();
builder.Services.AddDatabase(APISetting.UserDBConnection);
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
    List<OrgProdcut> orgProdcut = new List<OrgProdcut>
    {
        new OrgProdcut { ProductId = 1 }
    };

    await mediator.Send(new Menu_InstUpd { }, CancellationToken.None);

    await mediator.Send(new Prod_InstUpd { }, CancellationToken.None);

    await mediator.Send(new Adm_Org_InstUpd { Id = 0, OrgName = "Softexim", OrgEmail = "SofteximAdmin@sfotexim.com", IsActive = true, IsFreshSetup = true, OrgProdcut = orgProdcut }, CancellationToken.None);
}

app.Run();