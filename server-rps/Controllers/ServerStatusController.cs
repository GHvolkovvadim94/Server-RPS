using Microsoft.AspNetCore.Mvc;
using server_rps.Data;

namespace server_rps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerStatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServerStatusController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { message = "Server is running" });
        }
    }
}