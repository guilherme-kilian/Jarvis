using Jarvis.Clients;
using Jarvis.Handlers;

namespace Jarvis.Configurations
{
    public static class HttpClientConfigs
    {
        public static MauiAppBuilder AddHttpConfigs(this MauiAppBuilder builder, AppSettings? appSettings)
        {
            builder.Services.AddScoped<JarvisApiHandler>();

            builder.Services.AddHttpClient<IJarvisApiClient, JarvisApiClient>(client => client.BaseAddress = appSettings?.ApiUrl ?? new Uri("http://localhost:8080/api/v1/"))
                .AddHttpMessageHandler<JarvisApiHandler>();

            return builder;
        }
    }
}
