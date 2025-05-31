namespace Jarvis.Models.Tags
{
    public class CreateTagModel
    {
        public required string Title { get; set; }
        public required string Color { get; set; }
        public long UserId { get; set; }
    }
}
