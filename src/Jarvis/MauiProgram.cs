﻿using Jarvis.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
namespace Jarvis
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder
                .AddDependencyInjectionConfigs(out var appSettings)
                .AddLogConfigs()
                .AddHttpConfigs(appSettings);

            builder.Services.AddAuthorizationCore();

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddFluentUIComponents();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
