using Application_API;
using Application_Command.Insert_Command.UserConfig;
using Application_Domain;
using System.Net;
using System.Threading.Tasks;
using Test_Application.Common;
using Xunit;
using Xunit.Sdk;

namespace Test_Application.UserConfig
{
    public class Role_TestController : IClassFixture<ApplicationFactory<Startup>>
    {
        private readonly ApplicationFactory<Startup> _factory;

        public Role_TestController(ApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_Role()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            Role_Inst_cmd command = new()
            {
                RoleNmae = "Test",
                IsActive = true
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PostAsync($"/api/Role/Create", content);

            Response vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }

        [Fact]
        public async Task Update_Role()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            Role_Upd_cmd command = new()
            {
                RoleId = 1,
                RoleNmae = "Test Update",
                IsActive = true
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/Role/Update", content);

            Response vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }

        [Fact]
        public async Task Delete_Role()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();

            int Id = 1;

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/Role/Delete?Id=" + Id.ToString(), null);

            Response vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }

        [Fact]
        public async Task GetAll_Role()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();

            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/Role/GetAll");

            Response vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }

        [Fact]
        public async Task Get_Role()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            int Id = 1;
            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/Role/GetData?Id=" + Id.ToString());

            Response vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }
    }
}