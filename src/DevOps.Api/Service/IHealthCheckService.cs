using System.Threading.Tasks;

namespace Sizing.Poker.Api.Service
{
    public interface IHealthCheckService
    {
        Task<bool> Ping();
    }

    public class HealthCheckService : IHealthCheckService
    {
        public async Task<bool> Ping()
        {
            return await Task.FromResult(true);
        }
    }
}