using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sizing.Poker.Api.Service;

namespace Sizing.Poker.Api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController  : ControllerBase
    {
        private readonly IHealthCheckService healthCheckService;

        public HealthController(IHealthCheckService healthCheckService)
        {
            this.healthCheckService = healthCheckService;
        }

        [HttpGet("ping")]
        public async Task<ActionResult<bool>> GetAllUsersAsync()
        {
            var result = await healthCheckService.Ping();
            return Ok(result);
        }
    }
}