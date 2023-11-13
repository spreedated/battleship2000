using AudioLayer;
using Battleship2000.Models;
using neXn.Lib.ConfigurationHandler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleship2000.Logic
{
    internal static class RuntimeStorage
    {
        public static Assembly MyAssembly { get; } = typeof(RuntimeStorage).Assembly;
        public static string ProjectName { get; set; }
        public static string ProjectBuildAndVersion { get; set; }
        internal static ConfigurationHandler<Configuration> ConfigurationHandler { get; set; }
        internal static AudioEngine AudioEngine { get; set; }
        internal static List<Page> Pages { get; } = new();
        internal static List<Window> Windows { get; } = new();
        internal static Visibility BackgroundVis { get; set; } = Visibility.Collapsed;
        internal static ImageSource BackgroundImage { get; set; }
    }
}
