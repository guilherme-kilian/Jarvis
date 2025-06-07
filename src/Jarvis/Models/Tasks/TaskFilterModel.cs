using Jarvis.Converters;
using Jarvis.Models.Shared;
using Jarvis.Models.Tags;
using System.Text.Json.Serialization;

namespace Jarvis.Models.Tasks
{
    public class TaskFilterModel
    {
        public Priority? Priority { get; set; }

        public string? Title { get; set; }

        public List<long> Tags { get; set; } = [];

        [JsonPropertyName("filterStartDate")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? Start { get; set; }

        [JsonPropertyName("filterEndDate")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? End { get; set; }

        public bool? Completed { get; set; }
    }
}
