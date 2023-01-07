#pragma warning disable S1075

using Battleship2000.Models;
using Battleship2000.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        internal static Visibility BackgroundVis = Visibility.Collapsed;
        internal static ImageSource BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
    }
}
