using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Mock;
using Jarvis.Models.User;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface IUserClient
    {
        Task<UserModel> CreateUserAsync(CreateUserModel create);
        Task<UserModel> GetAsync(long id);
        Task<UserModel> UpdateAsync(long id, UpdateUserModel update);
        Task<UserModel?> CheckPasswordAsync(string email, string password);
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
            return await _client.PostFromJsonAsync<UserModel>("users", create);
        }

        public async Task<UserModel> GetAsync(long id)
        {
            var user = await _client.GetFromJsonAsync<UserModel>($"users/{id}", _options) 
                ?? throw new NotFoundException("Usuário não encontrado");

            return user;
        }

        public Task<UserModel?> CheckPasswordAsync(string email, string password)
        {
            return Task.FromResult(UsersMock.Users.FirstOrDefault(u => u.Email == email && u.Password == password));
        }

        public async Task<UserModel> UpdateAsync(long id, UpdateUserModel update)
        {
            return await _client.PutFromJsonAsync<UserModel>($"users/{id}", update);
        }
    }
}
