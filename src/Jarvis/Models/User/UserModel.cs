namespace Jarvis.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Password { get; set; }
        public string? ProfilePicture { get; set; }
        public int DailyGoalSize { get; set; }
    }
}
