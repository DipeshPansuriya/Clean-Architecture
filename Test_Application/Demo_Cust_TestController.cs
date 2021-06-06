using Application_API;
using Application_Command.Insert_Command;
using Application_Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Test_Application.Common;
using Xunit;
using Xunit.Sdk;

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
        public async Task CreateCustomer()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var command = new Demo_Customer_Inst_cmd
            {
                Code = "MM",
                Name = "This is Test"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/Demo_Customer/CreateCustomer", content);
            var vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);

                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                //Assert.False(response.StatusCode != HttpStatusCode.OK);
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }

        [Fact]
        public async Task GetAllCustomer()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var response = await client.GetAsync($"/api/Demo_Customer/GetAllCustomer");
            var vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                if (vm.ResponseObject != null)
                {
                    var data = JsonConvert.DeserializeObject<List<Demo_Customer>>(vm.ResponseObject.ToString());
                    Assert.NotEmpty(data);
                }
                else
                {
                    Assert.Empty(null);
                }
            }
            else
            {
                //Assert.False(response.StatusCode != HttpStatusCode.OK);
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                throw new XunitException(vm.ResponseMessage);
            }
        }

        [Fact]
        public async Task UpdateCustomer()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var command = new Demo_Customer_Upd_cmd
            {
                Id = 1,
                Code = "MM",
                Name = "This is Test"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/Demo_Customer/UpdateCustomer", content);
            var vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);

                Assert.Equal("success", vm.ResponseStatus.ToLower());
            }
            else
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                //throw new XunitException(vm.ResponseMessage);
            }
        }
    }
}