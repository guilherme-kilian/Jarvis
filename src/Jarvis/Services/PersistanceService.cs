using Jarvis.Models.User;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Jarvis.Services
{
    public class PersistanceService
    {
        private const string _userKey = "user_data";
        private readonly IJSRuntime _js;

        public PersistanceService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<UserModel?> TryGetFromStorageAsync()
        {
            var res = await _js.InvokeAsync<string?>("localStorage.getItem", _userKey);

            if (res is null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<UserModel>(res);
        }

        public async Task SetToStorageAsync(UserModel user)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", _userKey, JsonSerializer.Serialize(user));
        }

        public async Task ClearStorageAsync()
        {
            await _js.InvokeVoidAsync("localStorage.setItem", _userKey, null);
        }
    }
}
