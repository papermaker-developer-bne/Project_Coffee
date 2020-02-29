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

namespace Coffee.Test
{
    public class DrinkTest
    {
        public DrinkTest(ITestOutputHelper outputHelper)
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
            Output = outputHelper;
        }

        public HttpClient Client { get; }
        public ITestOutputHelper Output { get; }

        [Fact]
        public async Task DrinkList_Ok()
        {
            // Act
            var response = await Client.GetAsync($"/api/Drink/List");
            // Output
            var responseTest = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseTest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}