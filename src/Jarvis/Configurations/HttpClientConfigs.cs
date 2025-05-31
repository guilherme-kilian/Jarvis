using Jarvis.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Configurations
{
    public static class HttpClientConfigs
    {
        public static MauiAppBuilder AddHttpConfigs(this MauiAppBuilder builder, AppSettings appSettings)
        {
            builder.Services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = appSettings.ApiUrl);
            builder.Services.AddHttpClient<ITaskClient, TaskClient>(client => client.BaseAddress = appSettings.ApiUrl);
            builder.Services.AddHttpClient<ITagsClient, TagsClient>(client => client.BaseAddress = appSettings.ApiUrl);

            return builder;
        }
    }
}
