using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Models.Verifications
{
    public class ConfirmPhoneModel : VerifyPhoneModel
    {
        public required string Code { get; set; }
    }
}
