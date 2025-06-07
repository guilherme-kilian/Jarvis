using Jarvis.Models.Shared;
using Jarvis.Models.Substasks;
using Jarvis.Models.Tags;

namespace Jarvis.Models.Tasks
{
    public class TaskModel
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public bool IsEvent { get; set; }

        public bool Recurring { get; set; }

        public Recurrency Recurrence { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime LastOpened { get; set; }

        public bool Completed { get; set; }

        public Priority Priority { get; set; }

        public List<TagModel> Tags { get; set; } = [];

        public List<SubstaskModel> Subtasks { get; set; } = [];

        public int SubtaskCount { get; set; }

        public int CompletedCount;
    }
}
