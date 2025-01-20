using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPS.Models;
using server_rps.Data;
using server_rps.Models;


namespace server_rps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMatch([FromForm] string userId)
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
                Player1Id = player1.Id,
                Player2Id = player2.Id
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            var opponentId = player1.Id == userId ? player2.Id : player1.Id;

            return Ok(new { message = "Match created", opponentId = opponentId });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(string id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound(new { error = "Match not found" });
            }

            var player1 = await _context.Users.FindAsync(match.Player1Id);
            var player2 = await _context.Users.FindAsync(match.Player2Id);

            if (player1 != null) player1.State = UserState.Queue;
            if (player2 != null) player2.State = UserState.Queue;

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Match deleted" });
        }

        [HttpPost("battle")]
        public async Task<IActionResult> Battle([FromForm] string userId, [FromForm] Choice choice)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            // Логика обработки выбора игрока и определения результата раунда
            // Например:
            var result = DetermineRoundResult(choice);

            return Ok(new { result = result.ToString() });
        }

        private RoundResult DetermineRoundResult(Choice choice)
        {
            // Логика определения результата раунда
            // Например:
            if (choice == Choice.Rock)
            {
                return RoundResult.Win;
            }
            else if (choice == Choice.Paper)
            {
                return RoundResult.Lose;
            }
            else
            {
                return RoundResult.Draw;
            }
        }
    }
}