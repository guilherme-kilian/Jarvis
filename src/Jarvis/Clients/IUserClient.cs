using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Models.Auth;
using Jarvis.Models.User;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface IUserClient
    {
        Task<UserModel> CreateUserAsync(CreateUserModel create);
        Task<UserModel> GetAsync();
        Task<UserModel> UpdateAsync(UpdateUserModel update);
        Task<TokenModel> CheckPasswordAsync(LoginModel model);
        Task<UserModel> RawGetAsync(string token);
    }

    public class UserClient : IUserClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public UserClient(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<UserModel> CreateUserAsync(CreateUserModel create)
        {
            return await _client.PostFromJsonAsync<UserModel>("auth/registe", create);
        }

        public async Task<UserModel> GetAsync()
        {
            var user = await _client.GetFromJsonAsync<UserModel>("users", _options) 
                ?? throw new NotFoundException("Usuário não encontrado");

            return user;
        }

        public Task<TokenModel> CheckPasswordAsync(LoginModel model)
        {
            return _client.PostFromJsonAsync<TokenModel>("auth/login", model);
        }

        public async Task<UserModel> UpdateAsync(UpdateUserModel update)
        {
            return await _client.PutFromJsonAsync<UserModel>("users", update);
        }

        public async Task<UserModel> RawGetAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _client.GetFromJsonAsync<UserModel>("users", _options) ?? throw new NotFoundException("Usuário não encontrado");
        }
    }
}
