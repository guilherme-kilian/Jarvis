using Jarvis.Extensions;
using Jarvis.Models.Verifications;

namespace Jarvis.Clients
{
    public interface IIntegrationClient
    {
        Task<IntegrationModel?> GetAsync();

        Task VerifyPhoneAsync(VerifyPhoneModel model);

        Task ConfirmPhoneAsync(ConfirmPhoneModel model);
    }

    public class IntegrationClient : IIntegrationClient
    {
        private readonly HttpClient _client;

        public IntegrationClient(HttpClient client)
        {
            _client = client;
        }

        public async Task VerifyPhoneAsync(VerifyPhoneModel model)
        {
            _ = await _client.PostTextAsync("verification/send", model);
        }

        public async Task ConfirmPhoneAsync(ConfirmPhoneModel model)
        {
            _ = await _client.PostTextAsync("verification/confirm", model);
        }

        public Task<IntegrationModel?> GetAsync()
        {
            return _client.GetJsonAsync<IntegrationModel?>("verification");
        }
    }
}
