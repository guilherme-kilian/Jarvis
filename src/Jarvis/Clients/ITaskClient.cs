using Jarvis.Mock;
using Jarvis.Models.Tasks;

namespace Jarvis.Clients
{
    public interface ITaskClient
    {
        Task<List<TasksModel>> GetAsync();
        Task<List<TasksModel>> GetAsync(DateTime date);
        Task<List<TasksModel>> SearchAsync(TaskFilterModel filter);
        Task<TasksModel> CreateAsync(CreateTaskModel create);
    }

    public class FakeTaskClient : ITaskClient
    {
        public Task<TasksModel> CreateAsync(CreateTaskModel create)
        {
            throw new NotImplementedException();
        }

        public Task<List<TasksModel>> GetAsync()
        {
            return Task.FromResult(TaskMock.Tasks);
        }

        public Task<List<TasksModel>> GetAsync(DateTime date)
        {
            return Task.FromResult(TaskMock.Tasks);
        }

        public Task<List<TasksModel>> SearchAsync(TaskFilterModel filter)
        {
            return Task.FromResult(TaskMock.Tasks);
        }
    }
}
