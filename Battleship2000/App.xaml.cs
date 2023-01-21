#pragma warning disable S1075
#pragma warning disable IDE0079

using Battleship2000.Logic;
using Serilog;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly DateTime ApplicationStartupDate = DateTime.Now;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoggerConfigurator.ConfigureLogger();
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
