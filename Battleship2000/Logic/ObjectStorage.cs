using Battleship2000.Models;
using Battleship2000.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Logic
{
    internal static class ObjectStorage
    {
        public static string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public static string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version.ToNiceString()}";
        internal static Models.Configuration Config { get; set; } = new();
        internal readonly static List<Page> pages = new();
        internal readonly static List<Window> windows = new();
        internal readonly static LinkedList<Music> musics = new();
        internal readonly static List<EffectSound> sounds = new();
    }
}
