using Jarvis.Models.Shared;

namespace Jarvis.Models.Tasks
{
    public class TaskFilterModel
    {
        public Priority Priority { get; set; }

        public string? Title { get; set; }

        public List<string>? Tags { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public bool? Completed { get; set; }
    }
}
