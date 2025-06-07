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

        public WinTheDayClient(HttpClient client)
        {
            _client = client;
        }

        public async Task DecrementAsync(long taskId)
        {
            _ = await _client.DeleteTextAsync($"win-the-day/{taskId}");
        }

        public Task<WinTheDayModel> GetAsync()
        {
            return _client.GetJsonAsync<WinTheDayModel>("win-the-day");
        }

        public async Task IncrementAsync(long taskId)
        {
            _ = await _client.PostTextAsync($"win-the-day/{taskId}", null);
        }
    }
}
