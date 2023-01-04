using Serilog;
using Serilog.Events;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Battleship2000
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#if DEBUG
        private LogEventLevel level = LogEventLevel.Verbose;
#else
        private LogEventLevel level = LogEventLevel.Information;
#endif
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string logfilepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs", "logfile.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Debug(restrictedToMinimumLevel: level)
                .WriteTo.File(logfilepath, restrictedToMinimumLevel: level, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1048576)
                .CreateLogger();

            Log.Debug("[OnStartup] Log initialize");
        }
    }
}
