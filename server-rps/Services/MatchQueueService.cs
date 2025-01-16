using server_rps.Models;
using System.Collections.Generic;
using System.Linq;

public class MatchQueueService
{
    public Queue<User> MatchQueue { get; } = new Queue<User>();

    public void RemoveFromQueue(string userId)
    {
        var users = MatchQueue.Where(u => u.Id != userId).ToList();
        MatchQueue.Clear();
        foreach (var user in users)
        {
            MatchQueue.Enqueue(user);
        }
    }
}