using Serilog;

namespace AutomationFramework.Providers
{
    public static class LoggerProvider
    {
        private static readonly AsyncLocal<ILogger> LoggerContext = new();

        public static ILogger GetLogger()
        {
            return LoggerContext.Value ?? CreateLogger();
        }

        private static ILogger CreateLogger()
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console();

            return loggerConfig.CreateLogger();
        }
    }
}
