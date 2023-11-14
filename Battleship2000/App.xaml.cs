﻿#pragma warning disable S1075
#pragma warning disable IDE0079

using Battleship2000.Logic;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using static Battleship2000.Logic.RuntimeStorage;

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

            RuntimeStorage.ConfigurationHandler = new(new(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), "config.json")));
            RuntimeStorage.ConfigurationHandler.Load();

            RuntimeStorage.ProjectName = $"{MyAssembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title}";
            RuntimeStorage.ProjectBuildAndVersion = $"Built {HelperFunctions.LoadEmbeddedResource("gitid.txt", true)}/{HelperFunctions.LoadEmbeddedResource("builddate.txt", true).Replace(".", "")}/{MyAssembly.GetName().Version.ToNiceString()}";
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

            Log.Information($"Shuttung down application ... good bye!");
#pragma warning disable S6561
            Log.Information($"You've wasted {DateTime.Now - ApplicationStartupDate}");
#pragma warning restore S6561
            Log.CloseAndFlush();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && ((Button)sender).Content != null && ((Button)sender).Content.ToString().ToLower().Contains("test"))
            {
                return;
            }
            RuntimeStorage.AudioEngine.PlaySoundEffect("button-pressed");
        }
    }
}
