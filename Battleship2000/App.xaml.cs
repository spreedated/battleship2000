#pragma warning disable S1075

using Battleship2000.Logic;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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

            Log.Debug("[App] Log initialize");

            Configuration.Load();
        }

        private void DebugStartup(object sender, StartupEventArgs e)
        {
#if DEBUG
            //base.StartupUri = new Uri("pack://application:,,,/views/dialogwindow.xaml", UriKind.Absolute);
            base.StartupUri = new Uri("pack://application:,,,/views/MainWindow.xaml", UriKind.Absolute);
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
