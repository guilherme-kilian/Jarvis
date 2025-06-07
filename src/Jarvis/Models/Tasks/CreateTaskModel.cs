using Jarvis.Converters;
using Jarvis.Models.Shared;
using System.Text.Json.Serialization;

namespace Jarvis.Models.Tasks
{
    public class CreateTaskModel
    {
        public required string Title { get; set; }
        
        public Priority Priority { get; set; }
        
        public List<long> Tags { get; set; } = [];
        
        public bool Event { get; set; }
        
        public bool Recurring { get; set; }
        
        public Recurrency Recurrence { get; set; }

        [JsonConverter(typeof(DateTimeIsoConverter))]
        public DateTime Date { get; set; }
    }
}
