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
        internal static void ConfigureLogger()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location ?? Environment.ProcessPath;
            string logfilepath = Path.Combine(Path.GetDirectoryName(assemblyLocation), "logs", "logfile.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.SQLite(Path.Combine(Path.GetDirectoryName(logfilepath), "log.db"), tableName: "logs", restrictedToMinimumLevel: level)
#if DEBUG
                .WriteTo.Debug(restrictedToMinimumLevel: level)
#endif
                .WriteTo.File(logfilepath, restrictedToMinimumLevel: level, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1048576)
                .CreateLogger();

            Log.Debug("Logger initialize");
        }
    }
}
