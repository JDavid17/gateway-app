using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Newtonsoft.Json;

namespace gateway_app.IntegrationTest.API
{
    public class PeripheralAPITest
    {
        private readonly HttpClient _client;

        public PeripheralAPITest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllPeripheralsTest1(string httpMethod)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), "/api/peripherals");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async Task GetPeripheralByIdTest2(string httpMethod, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), $"/api/peripherals/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("PUT", 1)]
        public async Task UpdatePeripheralTest3(string httpMethod, int? id = null)
        {
            // Arrange
            // Arrange
            var payload = new Dictionary<string, string>
            {
                { "Id", "1"},
                {"UID", "777777" },
                {"Vendor", "CyberPower"},
                {"Date", "2020-12-06T17:30:59.0934534Z" },
                {"Status", "false" }
            };

            string strPaylod = JsonConvert.SerializeObject(payload, Formatting.Indented);
            HttpContent httpContent = new StringContent(strPaylod, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                Content = httpContent,
                RequestUri = new Uri($"https://localhost:44397/api/peripherals/{id}")
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task CreatePeripheralTest4(string httpMethod)
        {
            // Arrange
            var payload = new Dictionary<string, string>
            {
                {"UID", "111111" },
                {"Vendor", "I Buy Power"},
                {"Date", "2020-12-06T17:30:59.0934534Z" },
                {"Status", "true" }
            };

            string strPaylod = JsonConvert.SerializeObject(payload, Formatting.Indented);
            HttpContent httpContent = new StringContent(strPaylod, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                Content = httpContent,
                RequestUri = new Uri($"https://localhost:44397/api/peripherals")
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("DELETE", 1)]
        public async Task DeletePeripheralTest5(string httpMethod, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), $"/api/peripherals/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
