using System.Threading.Tasks;
using DevOps.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DevOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
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