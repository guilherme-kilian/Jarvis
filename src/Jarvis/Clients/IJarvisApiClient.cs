using Jarvis.Utils;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface IJarvisApiClient
    {
        ITagsClient Tags { get; }

        ITaskClient Tasks { get; }

        IUserClient Users { get; }
    }

    public class JarvisApiClient : IJarvisApiClient
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _options;

        public JarvisApiClient(HttpClient client)
        {
            _client = client;
            _options = JsonUtils.GetOptions();
        }

        public ITagsClient Tags => new TagsClient(_client, _options);

        public ITaskClient Tasks => new TaskClient(_client, _options);

        public IUserClient Users => new UserClient(_client, _options);
    }
}
