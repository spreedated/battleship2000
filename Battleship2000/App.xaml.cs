#pragma warning disable S1075
#pragma warning disable IDE0079

using Battleship2000.Logic;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Battleship2000.Logger.Enricher;

namespace Battleship2000
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#if DEBUG
        private readonly LogEventLevel level = LogEventLevel.Verbose;
#else
        private readonly LogEventLevel level = LogEventLevel.Information;
#endif
        private readonly DateTime ApplicationStartupDate = DateTime.Now;
        private const string logOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}][{Caller}] {Message}{NewLine}{Exception}";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#pragma warning disable IL3000
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
#pragma warning restore IL3000

            if (assemblyLocation == null)
            {
                assemblyLocation = Environment.ProcessPath;
            }

            string logfilepath = Path.Combine(Path.GetDirectoryName(assemblyLocation), "logs", "logfile.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithCaller()
#if DEBUG
                .WriteTo.Debug(restrictedToMinimumLevel: level, outputTemplate: logOutputTemplate)
#endif
                .WriteTo.File(logfilepath, restrictedToMinimumLevel: level, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1048576, outputTemplate: logOutputTemplate)
                .CreateLogger();

            Log.Debug("[App] Log initialize");

            ObjectStorage.ConfigurationHandler.Load();
        }

        private void DebugStartup(object sender, StartupEventArgs e)
        {
#if DEBUG
#pragma warning disable S125
            //base.StartupUri = new Uri("pack://application:,,,/views/dialogwindow.xaml", UriKind.Absolute);
            base.StartupUri = new Uri("pack://application:,,,/views/MainWindow.xaml", UriKind.Absolute);
#pragma warning restore S125
#else
            base.StartupUri = new Uri("pack://application:,,,/views/MainWindow.xaml", UriKind.Absolute);
#endif
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Log.Information($"[App] Shuttung down application ... good bye!");
            Log.Information($"[App] You've wasted {DateTime.Now - ApplicationStartupDate}");
            Log.CloseAndFlush();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && ((Button)sender).Content != null && ((Button)sender).Content.ToString().ToLower().Contains("test"))
            {
                return;
            }
            AudioEngine.PlaySoundEffect("button-pressed");
        }
    }
}
