using Jarvis.Clients;
using Jarvis.Providers;
using Jarvis.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jarvis.Configurations
{
    public static class DependencyInjectionConfigs
    {
        public static MauiAppBuilder AddDependencyInjectionConfigs(this MauiAppBuilder builder, out AppSettings? appSettings)
        {
            builder.Configuration.AddJsonFile("appsettings.json", optional: true);
            appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            builder.Services.AddSingleton<AuthenticatedUser>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.TryAddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();
            builder.Services.TryAddScoped<PersistanceService>();

            return builder;
        }
    }
}
