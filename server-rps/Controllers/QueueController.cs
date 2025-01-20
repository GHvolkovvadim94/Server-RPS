using Microsoft.AspNetCore.Mvc;
using server_rps.Data;
using server_rps.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace server_rps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QueueController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinQueue([FromForm] string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            user.State = UserState.Queue;
            await _context.SaveChangesAsync();

            return Ok(new { message = "User joined queue" });
        }

        [HttpPost("leave")]
        public async Task<IActionResult> LeaveQueue([FromForm] string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            user.State = UserState.Lobby;
            await _context.SaveChangesAsync();

            return Ok(new { message = "User left queue" });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetQueue()
        {
            var usersInQueue = await _context.Users
                .Where(u => u.State == UserState.Queue)
                .OrderBy(u => u.Id) // Добавьте OrderBy
                .Take(2)
                .ToListAsync();

            return Ok(usersInQueue);
        }
    }
}