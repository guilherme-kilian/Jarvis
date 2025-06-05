using Jarvis.Exceptions;
using Jarvis.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Extensions
{
    public static class HttpExtensions
    {
        public static Task<T> GetJsonAsync<T>(this HttpClient client, string path)
        {
            return SendAsync<T>(client, path, null, HttpVerbs.GET);
        }

        public static Task<T> DeleteJsonAsync<T>(this HttpClient client, string path)
        {
            return SendAsync<T>(client, path, null, HttpVerbs.DELETE);
        }

        public static Task<T> PostJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            return SendAsync<T>(client, path, body, HttpVerbs.POST);
        }

        public static Task<T> PutJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            return SendAsync<T>(client, path, body, HttpVerbs.PUT);
        }

        public static Task<T> PatchJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            return SendAsync<T>(client, path, body, HttpVerbs.PATCH);
        }

        private static async Task<T> SendAsync<T>(this HttpClient client, string path, object? body, HttpVerbs verb)
        {
            var options = JsonUtils.GetOptions();

            StringContent? stringContent = null;

            if (body != null)
            {
                stringContent = new StringContent(JsonSerializer.Serialize(body, options), System.Text.Encoding.UTF8, "application/json");
            }

            var response = verb switch
            {
                HttpVerbs.GET => await client.GetAsync(path),
                HttpVerbs.PUT => await client.PutAsync(path, stringContent),
                HttpVerbs.PATCH => await client.PatchAsync(path, stringContent),
                HttpVerbs.DELETE => await client.DeleteAsync(path),
                _ => await client.PostAsync(path, stringContent),
            };

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>(options) ?? throw new ArgumentNullException($"Object is null from request {path}");
        }

        private enum HttpVerbs
        {
            GET,
            POST,
            PUT,
            DELETE,
            HEAD,
            PATCH,
            OPTIONS,
        }
    }
}
