namespace Jarvis.Models.Reminders
{
    public class ReminderModel
    {
        public int Id { get; set; }

        public DateTime When { get; set; }

        public bool IsSent { get; set; }

        public required string Type { get; set; }
    }
}
