using Application_API;
using Application_API.Controllers;
using Application_Command.Insert_Command;
using Application_Core.Repositories;
using Application_Database;
using Application_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Application.Common;
using Test_Application.InitializeDbData;
using Xunit;

namespace Test_Application
{
    public class Demo_Cust_TestController : IClassFixture<ApplicationFactory<Startup>>
    {
        private readonly ApplicationFactory<Startup> _factory;

        public Demo_Cust_TestController(ApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var command = new Demo_Customer_Inst_cmd
            {
                Code = "MM",
                Name = "This is Test"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/Demo_Customer/PostValue", content);

            response.EnsureSuccessStatusCode();
        }
    }
}