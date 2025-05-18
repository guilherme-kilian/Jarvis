using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Models.User
{
    public class UpdateUserModel
    {
        public int DailyMeta { get; set; }
        public string? Name { get; set; }
        public string? PictureUrl { get; set; }
    }
}
