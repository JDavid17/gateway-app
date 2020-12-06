using System;
using Xunit;
using gateway_app.Models;
using gateway_app.MockData.Repositories;
using System.Threading.Tasks;

namespace gateway_app.UnitTest
{
    public class GatewayRepositoryTest
    {
        private readonly GatewayRepository _gatewayRepo;

        public GatewayRepositoryTest()
        {
            _gatewayRepo = new GatewayRepository();
        }

        [Fact]
        public async Task GatewayGetAllAsync()
        {
            // Arrange

            // Act
            var gateways = await _gatewayRepo.GetAllAsync();

            //Assert
            Assert.Single(gateways);
        }

        [Fact]
        public async Task GatewayGetByIdAsync()
        {
            //Arrange
            int id = 1;

            //Act
            var gateway = await _gatewayRepo.GetByIdAsync(id);

            //Assert
            Assert.Equal(id, gateway.Id);
        }
    }
}
