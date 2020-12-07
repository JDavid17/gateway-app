using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using System.Net;

namespace gateway_app.IntegrationTest.API
{
    public class GatewayAPITest
    {
        // HttpClient
        private readonly HttpClient _client;

        public GatewayAPITest()
        {
            // Mock Development Server
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllGatewaysTest1(string httpMethod)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), "/api/gateways");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async Task GetGatewayByIdTest2(string httpMethod, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), $"/api/gateways/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("PUT", 1)]
        public async Task UpdateGatewayTest3(string httpMethod, int? id = null)
        {
            // Arrange
            var payload = new Dictionary<string, string>
            {
                {"id", "1" },
                {"Name", "CISCO 01 Updated"},
                {"SerialNumber", "1234567" },
                {"Ipv4", "10.6.100.1" }
            };

            string strPaylod = JsonConvert.SerializeObject(payload, Formatting.Indented);
            HttpContent httpContent = new StringContent(strPaylod, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                Content = httpContent,
                RequestUri = new Uri($"https://localhost:44397/api/gateways/{id}")
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Theory]
        [InlineData("POST", 1)]
        public async Task AddPeripherialToGatewayTest4(string httpMethod, int? id = null)
        {
            // Arrange
            var payload = new Dictionary<string, string>
            {
                {"UID", "777777" },
                {"Vendor", "CyberPower"},
                {"Date", "2020-12-06T17:30:59.0934534Z" },
                {"Status", "true" }
            };

            string strPaylod = JsonConvert.SerializeObject(payload, Formatting.Indented);
            HttpContent httpContent = new StringContent(strPaylod, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                Content = httpContent,
                RequestUri = new Uri($"https://localhost:44397/api/gateways/{id}/add_peripheral")
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }


        [Theory]
        [InlineData("POST")]
        public async Task CreateGatewayTest5(string httpMethod)
        {
            // Arrange
            var payload = new Dictionary<string, string>
            {
                {"Name", "CISCO 09"},
                {"SerialNumber", "1234567777777" },
                {"Ipv4", "10.6.100.77" }
            };

            string strPaylod = JsonConvert.SerializeObject(payload, Formatting.Indented);
            HttpContent httpContent = new StringContent(strPaylod, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                Content = httpContent,
                RequestUri = new Uri("https://localhost:44397/api/gateways")
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("DELETE", 2)]
        public async Task DeleteGatewayTest6(string httpMethod, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), $"/api/gateways/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
