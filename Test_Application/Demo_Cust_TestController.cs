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
            this._factory = factory;
        }

        [Fact]
        public async Task CreateCustomer()
        {
            System.Net.Http.HttpClient client = await this._factory.GetAuthenticatedClientAsync();
            Demo_Customer_Inst_cmd command = new Demo_Customer_Inst_cmd
            {
                Code = "MM",
                Name = "This is Test"
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PostAsync($"/api/Demo_Customer/CreateCustomer", content);
            Response vm = await Utilities.GetResponseContent<Response>(response);
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
            System.Net.Http.HttpClient client = await this._factory.GetAuthenticatedClientAsync();

            System.Net.Http.HttpResponseMessage response = await client.GetAsync($"/api/Demo_Customer/GetAllCustomer");
            Response vm = await Utilities.GetResponseContent<Response>(response);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsType<Response>(vm);
                if (vm.ResponseObject != null)
                {
                    List<Demo_Customer> data = JsonConvert.DeserializeObject<List<Demo_Customer>>(vm.ResponseObject.ToString());
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
            System.Net.Http.HttpClient client = await this._factory.GetAuthenticatedClientAsync();
            Demo_Customer_Upd_cmd command = new Demo_Customer_Upd_cmd
            {
                Id = 1,
                Code = "MM",
                Name = "This is Test"
            };

            System.Net.Http.StringContent content = Utilities.GetRequestContent(command);

            System.Net.Http.HttpResponseMessage response = await client.PutAsync($"/api/Demo_Customer/UpdateCustomer", content);
            Response vm = await Utilities.GetResponseContent<Response>(response);
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