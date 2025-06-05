using Jarvis.Models.Shared;
using Jarvis.Models.Substasks;
using Jarvis.Models.Tags;

namespace Jarvis.Models.Tasks
{
    public class UpdateTaskModel
    {
        public required string Title { get; set; }

        public bool IsEvent { get; set; }

        public bool Recurring { get; set; }

        public Recurrency Recurrence { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public Priority Priority { get; set; }

        public List<TagModel> Tags { get; set; } = [];

        public List<SubstaskModel> Subtasks = [];
    }
}
