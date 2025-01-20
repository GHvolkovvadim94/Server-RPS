using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_rps.Data;
using server_rps.Models;
using System.Text.RegularExpressions;
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
            if (!IsValidName(name))
            {
                return BadRequest(new { error = "Name must be 1-16 characters long and contain only letters, numbers, and no spaces." });
            }

            var user = new User { Name = name };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully", id = user.Id });
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

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] string newName)
        {
            if (!IsValidName(newName))
            {
                return BadRequest(new { error = "Name must be 1-16 characters long and contain only letters, numbers, and no spaces." });
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

        [HttpGet("name/{id}")]
        public async Task<IActionResult> GetName(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            return Ok(new { name = user.Name });
        }

        [HttpGet("validate/{userId}")]
        public async Task<IActionResult> Validate(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            return Ok(new { message = "User validated" });
        }

        private bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 16)
            {
                return false;
            }

            string pattern = @"^[a-zA-Zа-яА-Я0-9]+$";
            return Regex.IsMatch(name, pattern);
        }
    }
}