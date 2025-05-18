using Jarvis.Clients;
using Jarvis.Providers;
using Jarvis.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Configurations
{
    public static class DependencyInjectionConfigs
    {
        public static MauiAppBuilder AddDependencyInjectionConfigs(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<IUserClient, FakeUserClient>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.TryAddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();

            return builder;
        }
    }
}
