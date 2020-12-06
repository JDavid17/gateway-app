using gateway_app.MockData.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace gateway_app.UnitTest
{
    public class PeripheralRepositoryTest
    {
        private readonly PeripheralRepository _peripheralRepository;

        public PeripheralRepositoryTest()
        {
            _peripheralRepository = new PeripheralRepository();
        }

        [Fact]
        public async Task PeripheralGetAllAsync()
        {
            // Arrange

            // Act
            var peripherals = await _peripheralRepository.GetAllAsync();

            // Assert
            Assert.Single(peripherals);
        }

        [Fact]
        public async Task PeripheralGetByIdAsync()
        {
            // Arrange
            int id = 1;

            // Act
            var peripheral = await _peripheralRepository.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, peripheral.Id);
        }

        [Fact]
        public async Task PeripheralGetByGatewayId()
        {
            // Arrange
            int id = 1;

            // Act
            var peripherals = await _peripheralRepository.GetByGatewayIdAsync(id);

            // Assert
            Assert.Single(peripherals);
        }
    }
}
