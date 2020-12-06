using gateway_app.MockData.IRepositories;
using gateway_app.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gateway_app.MockData.Repositories
{
    public class GatewayRepository : IGatewayRepository
    {
        public void Dispose()
        {
        }
        
        public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => Task.FromResult(true);

        public Task<bool> UpdateAsync(Gateway gateway, CancellationToken ct = default) => Task.FromResult(true);
        
        public Task<Gateway> AddAsync(Gateway newGateway, CancellationToken ct = default)
        {
            newGateway.Id = 1;
            return newGateway.AsTask();
        }

        public Task<List<Gateway>> GetAllAsync(CancellationToken ct = default)
            => new Gateway
            {
                Id = 1,
                Ipv4 = "10.6.100.71",
                Name = "Gateway 1",
                SerialNumber = "1234567890-1234567890"
            }.AsListTask();

        public Task<Gateway> GetByIdAsync(int id, CancellationToken ct = default)
            => new Gateway
            {
                Id = id,
                Ipv4 = "10.6.100.71",
                Name = "Gateway",
                SerialNumber = "1234567890-1234567890"
            }.AsTask();
    }
}
