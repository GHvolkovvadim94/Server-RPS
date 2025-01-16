namespace server_rps.Models
{
    public enum UserState
    {
        Lobby,
        Queue,
        InGame
    }

    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserState State { get; set; } = UserState.Lobby;
    }
}