namespace Jarvis.Models.Substasks
{
    public class SubstaskModel
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public bool IsDone { get; set; }
    }

    public class AddSubtaskModel
    {
        public required string Title { get; set; }

        public bool Completed { get; set; }
    }
}
