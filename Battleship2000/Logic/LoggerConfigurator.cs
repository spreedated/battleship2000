using Battleship2000.Logger.Enricher;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;

namespace Battleship2000.Logic
{
    internal static class LoggerConfigurator
    {
#if DEBUG
        private static readonly LogEventLevel level = LogEventLevel.Verbose;
#else
        private static readonly LogEventLevel level = LogEventLevel.Information;
#endif
        private const string logOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}][{Caller}] {Message}{NewLine}{Exception}";

        internal static void ConfigureLogger()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location ?? Environment.ProcessPath;
            string logfilepath = Path.Combine(Path.GetDirectoryName(assemblyLocation), "logs", "logfile.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithCaller()
                .WriteTo.SQLite(Path.Combine(Path.GetDirectoryName(logfilepath), "log.db"), tableName: "logs", restrictedToMinimumLevel: level)
#if DEBUG
                .WriteTo.Debug(restrictedToMinimumLevel: level, outputTemplate: logOutputTemplate)
#endif
                .WriteTo.File(logfilepath, restrictedToMinimumLevel: level, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1048576, outputTemplate: logOutputTemplate)
                .CreateLogger();

            Log.Debug("Logger initialize");
        }
    }
}
