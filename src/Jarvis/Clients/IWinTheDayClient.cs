using Jarvis.Extensions;
using Jarvis.Models.WinTheDays;

namespace Jarvis.Clients
{
    public interface IWinTheDayClient
    {
        Task<WinTheDayModel> GetAsync(DateTime date);
        Task IncrementAsync(long taskId, DateTime date);
        Task DecrementAsync(long taskId, DateTime date);
    }

    public class WinTheDayClient : IWinTheDayClient
    {
        private readonly HttpClient _client;

        public WinTheDayClient(HttpClient client)
        {
            _client = client;
        }

        public async Task DecrementAsync(long taskId, DateTime date)
        {
            _ = await _client.DeleteTextAsync(ConcatDateQueryString($"win-the-day/{taskId}", date));
        }

        public Task<WinTheDayModel> GetAsync(DateTime date)
        {
            return _client.GetJsonAsync<WinTheDayModel>(ConcatDateQueryString("win-the-day", date));
        }

        public async Task IncrementAsync(long taskId, DateTime date)
        {
            _ = await _client.PostTextAsync(ConcatDateQueryString($"win-the-day/{taskId}", date), null);
        }

        private string ConcatDateQueryString(string path, DateTime date)
        {
            return $"{path}?date={date:yyyy-MM-dd}";
        }
    }
}
