using gateway_app.MockData.IRepositories;
using gateway_app.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gateway_app.MockData.Repositories
{
    public class PeripheralRepository : IPeripheralRepository
    {
        public void Dispose()
        {
            
        }

        public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => Task.FromResult(true);

        public Task<bool> UpdateAsync(Peripheral peripheral, CancellationToken ct = default) => Task.FromResult(true);

        public Task<Peripheral> AddAsync(Peripheral newPeripheral, CancellationToken ct = default)
        {
            newPeripheral.Id = 1;
            return newPeripheral.AsTask();
        }

        public Task<List<Peripheral>> GetAllAsync(CancellationToken ct = default)
            => new Peripheral
            {
                Id = 1,
                GatewayId = 1,
                Date = DateTime.Now,
                Status = true,
                UID = 7777,
                Vendor = "Asus"
            }.AsListTask();

        public Task<List<Peripheral>> GetByGatewayIdAsync(int id, CancellationToken ct = default)
            => new Peripheral
            {
                GatewayId = id,
                Id = 1,
                Date = DateTime.Now,
                Status = true,
                UID = 7777,
                Vendor = "Asus"
            }.AsListTask();

        public Task<Peripheral> GetByIdAsync(int id, CancellationToken ct = default)
            => new Peripheral
            {
                Id = id,
                GatewayId = 1,
                Date = DateTime.Now,
                Status = true,
                UID = 7777,
                Vendor = "Asus"
            }.AsTask();
    }
}
