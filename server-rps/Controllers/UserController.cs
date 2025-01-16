using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_rps.Data;
using server_rps.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace server_rps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { error = "Name is required" });
            }

            var userId = Guid.NewGuid().ToString();
            var user = new User { Id = userId, Name = name, State = UserState.Lobby };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { id = userId, name = name });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (user.State != UserState.Lobby)
            {
                return BadRequest(new { error = "Cannot delete user who is not in the lobby" });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User deleted successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest(new { error = "New name is required" });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (user.State != UserState.Lobby)
            {
                return BadRequest(new { error = "Cannot update name of user who is not in the lobby" });
            }

            user.Name = newName;
            await _context.SaveChangesAsync();
            return Ok(new { id = user.Id, name = user.Name });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("queue/{id}")]
        public async Task<IActionResult> AddToQueue(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (user.State != UserState.Lobby)
            {
                return BadRequest(new { error = "User is not in the lobby" });
            }

            user.State = UserState.Queue;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User added to the queue" });
        }

        [HttpDelete("queue/{id}")]
        public async Task<IActionResult> RemoveFromQueue(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (user.State != UserState.Queue)
            {
                return BadRequest(new { error = "User is not in the queue" });
            }

            user.State = UserState.Lobby;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User removed from the queue" });
        }

        [HttpPost("match/create")]
        public async Task<IActionResult> CreateMatch()
        {
            var usersInQueue = await _context.Users.Where(u => u.State == UserState.Queue).Take(2).ToListAsync();
            if (usersInQueue.Count < 2)
            {
                return BadRequest(new { error = "Not enough players in the queue" });
            }

            var player1 = usersInQueue[0];
            var player2 = usersInQueue[1];

            player1.State = UserState.InGame;
            player2.State = UserState.InGame;

            var match = new Match
            {
                Id = Guid.NewGuid().ToString(),
                Player1Id = player1.Id,
                Player2Id = player2.Id
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Match created", matchId = match.Id });
        }

        [HttpDelete("match/{id}")]
        public async Task<IActionResult> DeleteMatch(string id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound(new { error = "Match not found" });
            }

            var player1 = await _context.Users.FindAsync(match.Player1Id);
            var player2 = await _context.Users.FindAsync(match.Player2Id);

            if (player1 != null) player1.State = UserState.Lobby;
            if (player2 != null) player2.State = UserState.Lobby;

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Match deleted successfully" });
        }

        [HttpGet("queue")]
        public async Task<IActionResult> GetQueue()
        {
            var queue = await _context.Users.Where(u => u.State == UserState.Queue).ToListAsync();
            return Ok(queue);
        }

        [HttpGet("matches")]
        public async Task<IActionResult> GetMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            return Ok(matches);
        }
    }
}