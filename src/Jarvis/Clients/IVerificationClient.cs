using Jarvis.Extensions;
using Jarvis.Models.Verifications;

namespace Jarvis.Clients
{
    public interface IVerificationClient
    {
        Task VerifyPhoneAsync(VerifyPhoneModel model);

        Task ConfirmPhoneAsync(ConfirmPhoneModel model);
    }

    public class VerificationClient : IVerificationClient
    {
        private readonly HttpClient _client;

        public VerificationClient(HttpClient client)
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
    }
}
