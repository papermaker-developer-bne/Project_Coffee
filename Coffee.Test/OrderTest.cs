using Coffee.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Coffee.API.XUnitTest
{
    public class OrderTest
    {
        public OrderTest(ITestOutputHelper outputHelper)
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
            Output = outputHelper;
        }

        public HttpClient Client { get; }
        public ITestOutputHelper Output { get; }

        [Fact]
        public async Task OrderList_Ok()
        {
            // Act
            var response = await Client.GetAsync($"/api/Order/List");
            // Output
            var responseTest = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseTest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OrderAll_Ok()
        {
            // Act
            var response = await Client.GetAsync($"/api/Order/All");
            // Output
            var responseTest = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseTest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OrderInfo_Ok()
        {
            // Arrange
            string name = "coach";
            // Act
            var response = await Client.GetAsync($"/api/Order/Info?name=" + name);
            // Output
            var responseTest = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseTest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
