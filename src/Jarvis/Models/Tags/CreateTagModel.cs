using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Tags
{
    public class CreateTagModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Color { get; set; }

        public long UserId { get; set; }
    }
}
