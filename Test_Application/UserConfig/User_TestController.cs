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
    public class User_TestController : IClassFixture<ApplicationFactory<Startup>>
    {
        private readonly ApplicationFactory<Startup> _factory;

        public User_TestController(ApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_User()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            User_Inst_cmd command = new()
            {
                EmailId = "tet@test.com",
                UserName = "Test Test",
                Password = "Test@123",
                IsActive = true
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PostAsync($"/api/User/Create", content);

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
        public async Task Update_User()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            User_Upd_cmd command = new()
            {
                UserId = 1,
                EmailId = "tet@test.com",
                UserName = "Test Test",
                Password = "Test@123",
                IsActive = true
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/User/Update", content);

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
        public async Task Delete_User()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();

            int Id = 1;

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/User/Delete?Id=" + Id.ToString(), null);

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
        public async Task GetAll_User()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();

            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/User/GetAll");

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
        public async Task Get_User()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            int Id = 1;
            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/User/GetData?Id=" + Id.ToString());

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