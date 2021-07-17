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
    public class Right_TestController : IClassFixture<ApplicationFactory<Startup>>
    {
        private readonly ApplicationFactory<Startup> _factory;

        public Right_TestController(ApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_Right()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            Right_Inst_cmd command = new()
            {
                RoleId = 1,
                MenuId = 1,
                Add = true,
                View = true,
                Delete = true,
                Edit = true,
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PostAsync($"/api/Right/Create", content);

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
        public async Task Update_Right()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            Right_Upd_cmd command = new()
            {
                RightId = 5,
                RoleId = 1,
                MenuId = 1,
                Add = false,
                View = false,
                Delete = false,
                Edit = false,
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/Right/Update", content);

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
        public async Task Delete_Right()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();

            int Id = 1;

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/Right/Delete?Id=" + Id.ToString(), null);

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
        public async Task GetAll_Right()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();

            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/Right/GetAll");

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
        public async Task Get_Right()
        {
            System.Net.Http.HttpClient client = await _factory.GetAuthenticatedClientAsync();
            int Id = 5;
            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/Right/GetData?Id=" + Id.ToString());

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