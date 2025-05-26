using Microsoft.Extensions.Logging;

using MetroLog.Maui;
using MetroLog.MicrosoftExtensions;

namespace Jarvis.Configurations
{
    public static class LogConfigs
    {
        public static MauiAppBuilder AddLogConfigs(this MauiAppBuilder builder)
        {
            builder.Logging.AddConsoleLogger(_ => { });
            builder.Logging.AddInMemoryLogger(_ => { });
            builder.Logging.AddStreamingFileLogger(_ => { });
            return builder;
        }
    }
}
