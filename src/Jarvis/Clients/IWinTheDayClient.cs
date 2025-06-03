using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Models.WinTheDays;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface IWinTheDayClient
    {
        Task<WinTheDayModel> GetAsync();
        Task IncrementAsync(long taskId);
        Task DecrementAsync(long taskId);
    }

    public class WinTheDayClient : IWinTheDayClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public WinTheDayClient(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task DecrementAsync(long taskId)
        {
            _ = await _client.DeleteFromJsonAsync<string>($"win-the-day/{taskId}", _options) ?? throw new NotFoundException("WinTheDay not found");
        }

        public async Task<WinTheDayModel> GetAsync()
        {
            return await _client.GetFromJsonAsync<WinTheDayModel>("win-the-day", _options) ?? throw new NotFoundException("WinTheDay not found");
        }

        public Task IncrementAsync(long taskId)
        {
            return _client.PostFromJsonAsync<string>($"win-the-day/{taskId}", null);
        }
    }
}
