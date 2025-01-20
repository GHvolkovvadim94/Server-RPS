using System;
using NanoidDotNet;

namespace server_rps.Models
{
    public class Match
    {
        public string Id { get; set; } = Nanoid.Generate();
        public string Player1Id { get; set; } = string.Empty;
        public string Player2Id { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int Player1Health { get; set; } = 100;
        public int Player2Health { get; set; } = 100;
        public bool Player1Ready { get; set; } = false;
        public bool Player2Ready { get; set; } = false;
        public string Player1Choice { get; set; } = string.Empty;
        public string Player2Choice { get; set; } = string.Empty;
        public int CurrentRound { get; set; } = 1;

    }
}