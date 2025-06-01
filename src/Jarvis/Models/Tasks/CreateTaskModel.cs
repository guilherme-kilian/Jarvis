using Jarvis.Models.Shared;
using Jarvis.Models.Tags;
using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Tasks
{
    public class CreateTaskModel
    {
        [Required]
        public string? Title { get; set; }
        
        public Priority Priority { get; set; }
        
        public List<TagModel> Tags { get; set; } = [];
        
        public bool IsEvent { get; set; }
        
        public bool IsRecurrent { get; set; }
        
        public Recurrency Recurrent { get; set; }

        [Required]
        public DateTime? Date { get; set; }
    }
}
