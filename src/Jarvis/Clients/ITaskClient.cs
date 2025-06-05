using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Models.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

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
            return _client.PostJsonAsync<TaskModel>("tasks", create);
        }
               
        public Task<List<TaskModel>> GetAsync(DateTime date)
        {
            return SearchAsync(new TaskFilterModel()
            {
                Start = date.Date,
                End = date.Date.AddDays(1).AddTicks(-1),
            });
        }

        public Task<TaskModel> GetAsync(long id)
        {
            return _client.GetJsonAsync<TaskModel>($"tasks/{id}");
        }

        public Task<List<TaskModel>> SearchAsync(TaskFilterModel filter)
        {
            return _client.PostJsonAsync<List<TaskModel>>("tasks/filter", filter);
        }

        public Task<TaskModel> UpdateAsync(long id, UpdateTaskModel update)
        {
            return _client.PutJsonAsync<TaskModel>($"tasks/{id}", update);
        }

        public Task<int> DeleteAsync(long id)
        {
            return _client.DeleteJsonAsync<int>($"tasks/{id}");
        }

        public Task<TaskModel> UpdateStatusAsync(long id)
        {
            return _client.PatchJsonAsync<TaskModel>($"tasks/{id}/status", null);
        }
    }
}
