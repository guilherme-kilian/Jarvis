using Jarvis.Models.Tasks;

namespace Jarvis.Mock
{
    public static class TaskMock
    {
        public static List<TasksModel> Tasks =
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
                Labels =
                [ TagsMock.UnisinosLabel ],
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Title = "Corrigir TCC",
                Id = 2,
                IsDone = false,
                Labels = [ TagsMock.UnisinosLabel ],
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Title = "Limpar Quarto",
                Id = 2,
                IsDone = true,
                Labels = [TagsMock.UrgentLabel, TagsMock.HomeLabel],
                CreatedAt = DateTime.Now,
            },
            ];
    }
}
