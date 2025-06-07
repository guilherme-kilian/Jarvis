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
            var res = await _client.DeleteAsync($"win-the-day/{taskId}");

            if (!res.IsSuccessStatusCode)
            {
                throw new HttpException(res.StatusCode, await res.Content.ReadAsStringAsync());
            }
        }

        public Task<WinTheDayModel> GetAsync()
        {
            return _client.GetJsonAsync<WinTheDayModel>("win-the-day");
        }

        public async Task IncrementAsync(long taskId)
        {
            var res = await _client.PostAsync($"win-the-day/{taskId}", null);

            if (!res.IsSuccessStatusCode)
            {
                throw new HttpException(res.StatusCode, await res.Content.ReadAsStringAsync());
            }
        }
    }
}
