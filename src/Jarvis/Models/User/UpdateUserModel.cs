namespace Jarvis.Models.User
{
    public class UpdateUserModel
    {
        public int WinTheDayGoal { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? PictureUrl { get; set; }
    }
}
