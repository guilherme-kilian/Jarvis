using Jarvis.Models.Shared;

namespace Jarvis.Models.Tasks
{
    public class TaskFilterModel
    {
        public Priority Priority { get; set; }

        public string? Title { get; set; }

        public List<string>? Labels { get; set; }
    }
}
