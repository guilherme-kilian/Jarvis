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
        private readonly JsonSerializerOptions _options;

        public TagsClient(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<TagModel>> GetAsync()
        {
            return await _client.GetFromJsonAsync<List<TagModel>>("tags", _options)
                ?? throw new NotFoundException("Tags não encontradas");
        }

        public async Task<TagModel> GetAsync(long id)
        {
            return await _client.GetFromJsonAsync<TagModel>($"tags/{id}", _options)
                ?? throw new NotFoundException("Tag não encontrada");
        }

        public Task<TagModel> CreateAsync(CreateTagModel create)
        {
            return _client.PostFromJsonAsync<TagModel>("tags", create);
        }

        public Task<TagModel> UpdateAsync(long id, UpdateTagModel update)
        {
            return _client.PutFromJsonAsync<TagModel>($"tags/{id}", update);
        }

        public async Task DeleteAsync(long id)
        {
            var response = await _client.DeleteAsync($"tags/{id}");

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
