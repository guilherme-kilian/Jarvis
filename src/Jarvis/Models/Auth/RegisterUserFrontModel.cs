using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Auth
{
    public class RegisterUserFrontModel : LoginFrontModel
    {
        [Required]
        public string? Name { get; set; }
    }
}
