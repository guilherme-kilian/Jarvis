using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Auth
{
    public class LoginFrontModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }
    }
}
