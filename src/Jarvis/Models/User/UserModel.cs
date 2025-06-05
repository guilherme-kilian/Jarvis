namespace Jarvis.Models.User
{
    public class FullUserModel : UserModel
    {
        //public long Id { get; set; }
        //public required string Email { get; set; }
        public required string Token { get; set; }
    }

    public class UserModel
    {        
        public required string Name { get; set; }
        public required string LastName { get; set; }        
        public int? WinTheDayGoal { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
