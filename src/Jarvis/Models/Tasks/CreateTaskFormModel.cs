using Jarvis.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Jarvis.Models.Tasks
{
    public class CreateTaskFormModel
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
        public DateTime? Date { get; set; }

        public DateTime? Time { get; set; }

        public CreateTaskModel? ToApiModel()
        {
            if (Date is null || Priority is null || Title is null)
                return null;

            var date = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day, Time?.Hour ?? 0, Time?.Minute ?? 0, Time?.Second ?? 0);

            return new()
            {
                Date = date,
                Event = Event,
                Priority = Priority.Value,
                Recurrence = Recurrence,
                Recurring = Recurring,
                Tags = Tags,
                Title = Title,
            };
        }
    }
}
