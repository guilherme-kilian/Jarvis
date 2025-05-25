using Jarvis.Mock;
using Jarvis.Models.Tags;

namespace Jarvis.Clients
{
    public interface ITagsClient
    {
        Task<List<TagModel>> GetAsync();
    }

    public class FakeTagsClient : ITagsClient
    {
        public Task<List<TagModel>> GetAsync()
        {
            return Task.FromResult(TagsMock.Tags);
        }
    }
}
