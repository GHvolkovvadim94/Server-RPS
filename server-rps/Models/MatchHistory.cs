namespace server_rps.Models
{
    public class MatchHistory
    {
        public string Id { get; set; } = string.Empty;
        public string Player1Id { get; set; } = string.Empty;
        public string Player2Id { get; set; } = string.Empty;
        public int Rounds { get; set; } = 0;
        public string WinnerId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

    }
}