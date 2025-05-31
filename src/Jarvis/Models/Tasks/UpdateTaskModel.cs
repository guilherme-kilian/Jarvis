using Jarvis.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Models.Tasks
{
    public class UpdateTaskModel
    {
        public required string Title { get; set; }

        public bool IsEvent { get; set; }

        public bool IsRecurrent { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public Priority Priority { get; set; }
    }
}
