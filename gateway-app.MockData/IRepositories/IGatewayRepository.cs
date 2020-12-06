using gateway_app.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gateway_app.MockData.IRepositories
{
    interface IGatewayRepository : IDisposable
    {
        Task<List<Gateway>> GetAllAsync(CancellationToken ct = default);
        Task<Gateway> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Gateway> AddAsync(Gateway newGateway, CancellationToken ct = default);
        Task<bool> UpdateAsync(Gateway gateway, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
