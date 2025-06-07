using Jarvis.Converters;
using Jarvis.Models.Shared;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jarvis.Models.Tasks
{
    public class CreateTaskModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public Priority? Priority { get; set; }
        
        public List<long> Tags { get; set; } = [];
        
        public bool Event { get; set; }
        
        public bool Recurring { get; set; }
        
        public Recurrency Recurrence { get; set; }

        [Required]
        [JsonConverter(typeof(DateTimeIsoConverter))]
        public DateTime? Date { get; set; }
    }
}
