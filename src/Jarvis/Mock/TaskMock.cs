using Jarvis.Models.Tasks;

namespace Jarvis.Mock
{
    public static class TaskMock
    {
        public static List<TaskModel> Tasks =
            [
            new()
            {
                Title = "Escrever TCC",
                Id = 1,
                IsDone = false,
                Substasks =
                [
                    new() { Title = "Escrever introdução", Id = 1, IsDone = true },
                    new() { Title = "Escrever metodologia", Id = 2, IsDone = true },
                    new() { Title = "Escrever resultados", Id = 3, IsDone = true },
                    new() { Title = "Escrever referencial", Id = 4, IsDone = false },
                ],
                Tags =
                [ TagsMock.UnisinosLabel ],
                Date = DateTime.Now,
            },
            new()
            {
                Title = "Corrigir TCC",
                Id = 2,
                IsDone = false,
                Tags = [ TagsMock.UnisinosLabel ],
                Date = DateTime.Now,
            },
            new()
            {
                Title = "Limpar Quarto",
                Id = 2,
                IsDone = true,
                Tags = [TagsMock.UrgentLabel, TagsMock.HomeLabel],
                Date = DateTime.Now,
            },
            ];
    }
}
