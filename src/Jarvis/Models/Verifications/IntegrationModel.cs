using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Models.Verifications
{
    public class IntegrationModel
    {
        public long Id { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
