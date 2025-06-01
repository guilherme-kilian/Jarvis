using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Models.Tasks;
using System.Net.Http.Json;

namespace Jarvis.Clients
{
    public interface ITaskClient
    {
        Task<TaskModel> CreateAsync(CreateTaskModel create);
        Task<TaskModel> GetAsync(long id);
        Task<List<TaskModel>> GetAsync(DateTime date);
        Task<TaskModel> UpdateAsync(long id, UpdateTaskModel update);
        Task<TaskModel> UpdateStatusAsync(long id);
        Task<int> DeleteAsync(long id);
        Task<List<TaskModel>> SearchAsync(TaskFilterModel filter);
    }

    public class TaskClient : ITaskClient
    {
        private readonly HttpClient _client;

        public TaskClient(HttpClient client)
        {
            _client = client;
        }

        public Task<TaskModel> CreateAsync(CreateTaskModel create)
        {
            return _client.PostFromJsonAsync<TaskModel>($"tasks", create);
        }
               
        public Task<List<TaskModel>> GetAsync(DateTime date)
        {
            return SearchAsync(new TaskFilterModel()
            {
                Start = date.Date,
                End = date.Date.AddDays(1).AddTicks(-1),
            });
        }

        public async Task<TaskModel> GetAsync(long id)
        {
            return await _client.GetFromJsonAsync<TaskModel>($"tasks/{id}") ?? throw new NotFoundException("Task não encontrada");
        }

        public Task<List<TaskModel>> SearchAsync(TaskFilterModel filter)
        {
            return _client.PostFromJsonAsync<List<TaskModel>>("tasks/filter", filter);
        }

        public Task<TaskModel> UpdateAsync(long id, UpdateTaskModel update)
        {
            return _client.PutFromJsonAsync<TaskModel>($"tasks/{id}", update);
        }

        public async Task<int> DeleteAsync(long id)
        {
            return await _client.DeleteFromJsonAsync<int>($"tasks/{id}");
        }

        public Task<TaskModel> UpdateStatusAsync(long id)
        {
            return _client.PatchFromJsonAsync<TaskModel>($"tasks/{id}/status", null);
        }
    }
}
