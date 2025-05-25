using Jarvis.Models.Shared;
using Jarvis.Models.Tags;

namespace Jarvis.Models.Tasks
{
    public class CreateTaskModel
    {
        public required string Title { get; set; }
        public Priority Priority { get; set; }
        public List<TagModel> Labels { get; set; } = [];
        public bool IsEvent { get; set; }
    }
}
