using Jarvis.Converters;
using Jarvis.Models.Reminders;
using Jarvis.Models.Shared;
using Jarvis.Models.Substasks;
using Jarvis.Models.Tags;
using System.Text.Json.Serialization;

namespace Jarvis.Models.Tasks
{
    public class UpdateTaskModel
    {
        public required string Title { get; set; }

        public bool IsEvent { get; set; }

        public bool Recurring { get; set; }

        public Recurrency Recurrence { get; set; }

        [JsonConverter(typeof(DateTimeIsoConverter))]
        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public Priority Priority { get; set; }

        public bool Reminder { get; set; }

        public ReminderType ReminderType { get; set; }

        public List<long> Tags { get; set; } = [];


        public List<AddSubtaskModel> Subtasks = [];
    }
}
