using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Models.Tags;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface ITagsClient
    {
        Task<TagModel> CreateAsync(CreateTagModel create);
        Task<List<TagModel>> GetAsync();
        Task<TagModel> GetAsync(long id);
        Task<TagModel> UpdateAsync(long id, UpdateTagModel update);
        Task DeleteAsync(long id);
    }

    public class TagsClient : ITagsClient
    {
        private readonly HttpClient _client;

        public TagsClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TagModel>> GetAsync()
        {
            return await _client.GetJsonAsync<List<TagModel>>("tags");
        }

        public async Task<TagModel> GetAsync(long id)
        {
            return await _client.GetJsonAsync<TagModel>($"tags/{id}");
        }

        public Task<TagModel> CreateAsync(CreateTagModel create)
        {
            return _client.PostJsonAsync<TagModel>("tags", create);
        }

        public Task<TagModel> UpdateAsync(long id, UpdateTagModel update)
        {
            return _client.PutJsonAsync<TagModel>($"tags/{id}", update);
        }

        public Task DeleteAsync(long id)
        {
            return _client.DeleteJsonAsync<string>($"tags/{id}");
        }
    }
}
