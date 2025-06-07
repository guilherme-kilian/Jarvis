using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Verifications
{
    public class ConfirmPhoneFormModel : VerifyPhoneModel
    {
        [Required]
        public string? Code { get; set; }

        public ConfirmPhoneModel ToApiModel()
        {
            return new ConfirmPhoneModel
            {
                PhoneNumber = PhoneNumber,
                Code = Code ?? string.Empty,
            };
        }
    }
}
