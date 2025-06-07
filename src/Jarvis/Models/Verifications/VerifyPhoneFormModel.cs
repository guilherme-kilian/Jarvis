using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Verifications
{
    public class VerifyPhoneFormModel
    {
        private readonly string _ddi = "55";

        [MaxLength(4)]
        [Required]
        public string? DDD { get; set; } = "51";

        [MaxLength(9)]
        [Required]
        public string? PhoneNumber { get; set; }

        public VerifyPhoneModel ToApiModel()
        {
            return new VerifyPhoneModel
            {
                PhoneNumber = _ddi + DDD + PhoneNumber
            };
        }
    }
}
