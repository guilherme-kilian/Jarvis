using Jarvis.Exceptions;
using Jarvis.Extensions;
using Jarvis.Models.Auth;
using Jarvis.Models.User;
using Jarvis.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface IUserClient
    {
        Task<UserModel> CreateUserAsync(CreateUserModel create);
        Task<UserModel> GetAsync();
        Task<UserModel> UpdateAsync(UpdateUserModel update, Stream profilePicture, string profilePictureName);
        Task<TokenModel> CheckPasswordAsync(LoginModel model);
        Task<UserModel> RawGetAsync(string token);
    }

    public class UserClient : IUserClient
    {
        private readonly HttpClient _client;

        public UserClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserModel> CreateUserAsync(CreateUserModel create)
        {
            return await _client.PostJsonAsync<UserModel>("auth/register", create);
        }

        public Task<UserModel> GetAsync()
        {
            return _client.GetJsonAsync<UserModel>("user");
        }

        public Task<TokenModel> CheckPasswordAsync(LoginModel model)
        {
            return _client.PostJsonAsync<TokenModel>("auth/login", model);
        }

        public async Task<UserModel> UpdateAsync(UpdateUserModel update, Stream profilePicture, string profilePictureName)
        {
            var data = new MultipartFormDataContent();

            var userData = new StringContent(JsonSerializer.Serialize(update, JsonUtils.GetOptions()), System.Text.Encoding.UTF8, "application/json");

            data.Add(userData, "user");

            var profilePictureData = new StreamContent(profilePicture);

            profilePictureData.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");

            data.Add(profilePictureData, "file", profilePictureName);

            var response = await _client.PutAsync("user", data);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await GetAsync();
        }

        public async Task<UserModel> RawGetAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _client.GetJsonAsync<UserModel>("user");
        }
    }
}
