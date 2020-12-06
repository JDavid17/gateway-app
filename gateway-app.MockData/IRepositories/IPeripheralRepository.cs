using System;
using System.Collections.Generic;
using System.Text;
using gateway_app.Models;
using System.Threading.Tasks;
using System.Threading;

namespace gateway_app.MockData.IRepositories
{
    interface IPeripheralRepository : IDisposable
    {
        Task<List<Peripheral>> GetAllAsync(CancellationToken ct = default);
        Task<Peripheral> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<Peripheral>> GetByGatewayIdAsync(int id, CancellationToken ct = default);
        Task<Peripheral> AddAsync(Peripheral newPeripheral, CancellationToken ct = default);
        Task<bool> UpdateAsync(Peripheral peripheral, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
