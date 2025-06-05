using Jarvis.Models.Shared;
using Jarvis.Models.Tags;
using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Tasks
{
    public class CreateTaskModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public Priority? Priority { get; set; }
        
        public List<long> Tags { get; set; } = [];
        
        public bool IsEvent { get; set; }
        
        public bool Recurring { get; set; }
        
        public Recurrency Recurrence { get; set; }

        [Required]
        public DateTime? Date { get; set; }
    }
}
