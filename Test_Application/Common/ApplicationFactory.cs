using Application_Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Test_Application.InitializeDbData;

namespace Test_Application.Common
{
    public class ApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder
        //        .ConfigureServices(services =>
        //        {
        //            // Create a new service provider.
        //            ServiceProvider serviceProvider = new ServiceCollection()
        //                .AddEntityFrameworkInMemoryDatabase()
        //                .BuildServiceProvider();

        // // Add a database context using an in-memory database for testing.
        // services.AddDbContext<APP_DbContext>(options => {
        // options.UseInMemoryDatabase("InMemoryDbForTesting");
        // options.UseInternalServiceProvider(serviceProvider); });

        // //services.AddScoped<INorthwindDbContext>(provider => provider.GetService<NorthwindDbContext>());

        // ServiceProvider sp = services.BuildServiceProvider();

        // // Create a scope to obtain a reference to the database using IServiceScope scope =
        // sp.CreateScope(); IServiceProvider scopedServices = scope.ServiceProvider; APP_DbContext
        // context = scopedServices.GetRequiredService<APP_DbContext>();
        // ILogger<ApplicationFactory<TStartup>> logger = scopedServices.GetRequiredService<ILogger<ApplicationFactory<TStartup>>>();

        // // Ensure the database is created. context.Database.EnsureCreated();

        // try { // Seed the database with test data.

        //                //Test_User_Data.InitializeData(context);
        //            }
        //            catch (Exception ex)
        //            {
        //                logger.LogError(ex, "An error occurred seeding the " +
        //                                    $"database with test messages. Error: {ex.Message}");
        //            }
        //        })
        //        .UseEnvironment("Test");
        //}

        //public HttpClient GetAnonymousClient()
        //{
        //    return CreateClient();
        //}

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            string username = "";
            string password = "";
            return await GetAuthenticatedClientAsync(username, password);
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync(string userName, string password)
        {
            HttpClient client = CreateClient();

            //var token = await GetAccessTokenAsync(client, userName, password);

            //client.SetBearerToken(token);

            return client;
        }
    }
}