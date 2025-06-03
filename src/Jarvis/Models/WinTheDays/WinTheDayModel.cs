using Jarvis.Models.Tasks;

namespace Jarvis.Models.WinTheDays
{
    public class WinTheDayModel
    {
        public DateTime Day { get; set; }
        public List<TaskModel> Tasks { get; set; } = [];
        public int Completed { get; set; }
        public int TotalGoal { get; set; }
        public int RemainingTasksToSelect { get; set; }
    }
}
