using NanoidDotNet;

namespace server_rps.Models
{
    public class User
    {
        public string Id { get; set; } = Nanoid.Generate();
        public string Name { get; set; } = string.Empty;
        public UserState State { get; set; } = UserState.Lobby;
        public int Matches { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Draws { get; set; } = 0;
    }
}