namespace Jarvis.Models.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public int WinTheDayGoal { get; set; }
    }
}
