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
    public class ExpectedTest
    {
        public ExpectedTest(ITestOutputHelper outputHelper)
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
            Output = outputHelper;
        }

        public HttpClient Client { get; }
        public ITestOutputHelper Output { get; }

        [Fact]
        public async Task ExpectedAll_Ok()
        {

            var response = await Client.GetAsync($"/api/Results/All");

            // Output
            var responseTest = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseTest);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
