using Jarvis.Clients;

namespace Jarvis.Configurations
{
    public static class HttpClientConfigs
    {
        public static MauiAppBuilder AddHttpConfigs(this MauiAppBuilder builder, AppSettings appSettings)
        {
            builder.Services.AddHttpClient<IJarvisApiClient, JarvisApiClient>(client => client.BaseAddress = appSettings.ApiUrl);

            return builder;
        }
    }
}
