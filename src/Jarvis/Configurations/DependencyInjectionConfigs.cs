using Jarvis.Clients;
using Jarvis.Providers;
using Jarvis.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jarvis.Configurations
{
    public static class DependencyInjectionConfigs
    {
        public static MauiAppBuilder AddDependencyInjectionConfigs(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<IUserClient, FakeUserClient>();
            builder.Services.AddScoped<ITaskClient, FakeTaskClient>();
            builder.Services.AddScoped<ITagsClient, FakeTagsClient>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.TryAddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();
            builder.Services.TryAddScoped<PersistanceService>();

            return builder;
        }
    }
}
