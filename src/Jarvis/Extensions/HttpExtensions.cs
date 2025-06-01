using Jarvis.Exceptions;
using System.Net.Http.Json;

namespace Jarvis.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<T> PostFromJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            var response = await client.PostAsJsonAsync(path, body);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>() ?? throw new ArgumentNullException($"Object is null from request {path}");
        }

        public static async Task<T> PutFromJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            var response = await client.PutAsJsonAsync(path, body);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>() ?? throw new ArgumentNullException($"Object is null from request {path}");
        }

        public static async Task<T> PatchFromJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            var response = await client.PatchAsJsonAsync(path, body);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>() ?? throw new ArgumentNullException($"Object is null from request {path}");
        }
    }
}
