using Jarvis.Exceptions;
using Jarvis.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jarvis.Extensions
{
    public static class HttpExtensions
    {
        public static Task<T> PostFromJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            return SendAsync<T>(client, path, body, HttpVerbs.POST);
        }

        public static Task<T> PutFromJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            return SendAsync<T>(client, path, body, HttpVerbs.PUT);
        }

        public static Task<T> PatchFromJsonAsync<T>(this HttpClient client, string path, object? body)
        {
            return SendAsync<T>(client, path, body, HttpVerbs.PATCH);
        }

        private static async Task<T> SendAsync<T>(this HttpClient client, string path, object? body, HttpVerbs verb)
        {
            var options = JsonUtils.GetOptions();

            var stringContent = new StringContent(JsonSerializer.Serialize(body, options), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            switch (verb)
            {
                case HttpVerbs.PUT:
                    response = await client.PutAsync(path, stringContent);
                    break;
                case HttpVerbs.PATCH:
                    response = await client.PatchAsync(path, stringContent);
                    break;
                case HttpVerbs.POST:
                default:
                    response = await client.PostAsync(path, stringContent);
                    break;
            }

            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>(options) ?? throw new ArgumentNullException($"Object is null from request {path}");
        }

        private enum HttpVerbs
        {
            POST,
            PUT,
            PATCH,
        }
    }
}
