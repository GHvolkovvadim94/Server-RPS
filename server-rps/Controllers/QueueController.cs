using Microsoft.EntityFrameworkCore;
using server_rps.Data;
using server_rps.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_rps.Services
{
    public class QueueService
    {
        private readonly AppDbContext _context;

        public QueueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToQueue(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.State != UserState.Lobby)
            {
                return false;
            }

            user.State = UserState.Queue;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromQueue(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.State != UserState.Queue)
            {
                return false;
            }

            user.State = UserState.Lobby;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetQueue()
        {
            return await _context.Users
                .Where(u => u.State == UserState.Queue)
                .ToListAsync();
        }
    }
}
