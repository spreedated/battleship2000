#pragma warning disable IDE0079

using Battleship2000.Logic;
using EngineLayer;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using static Battleship2000.Logic.Globals;

namespace Battleship2000
{
    public partial class App : Application
    {
        private readonly DateTime ApplicationStartupDate = DateTime.Now;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoggerConfigurator.ConfigureLogger();

            Globals.ConfigurationHandler = new(new(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), "config.json")));
            Globals.ConfigurationHandler.Load();

            Globals.ProjectName = $"{MyAssembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title}";
            Globals.ProjectBuildAndVersion = $"Built {HelperFunctions.LoadEmbeddedResourceString(MyAssembly, "gitid.txt", true)}/{HelperFunctions.LoadEmbeddedResourceString(MyAssembly, "builddate.txt", true).Replace(".", "")}/{MyAssembly.GetName().Version.ToNiceString()}";
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
            Globals.AudioEngine.PlaySoundEffect("button-pressed");
        }
    }
}
