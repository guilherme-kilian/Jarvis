using Jarvis.Models.Shared;
using Jarvis.Models.Substasks;
using Jarvis.Models.Tags;

namespace Jarvis.Models.Tasks
{
    public class TasksModel
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public bool IsEvent { get; set; }

        public bool IsRecurrent { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastOpened { get; set; }

        public bool IsDone { get; set; }

        public Priority Priority { get; set; }

        public List<TagModel> Labels { get; set; } = [];
        public List<SubstaskModel> Substasks { get; set; } = [];
    }
}
