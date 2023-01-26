﻿#pragma warning disable S1075

using Battleship2000.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleship2000.Logic
{
    internal static class ObjectStorage
    {
        public static string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(ObjectStorage).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public static string ProjectVersion { get; } = $"v{typeof(ObjectStorage).Assembly.GetName().Version.ToNiceString()}";
        internal static ConfigurationHandler ConfigurationHandler { get; } = new(Path.Combine(Path.GetDirectoryName(typeof(ObjectStorage).Assembly.Location), "config.json"));
        internal static Models.Configuration Config { get; set; }
        internal static List<Page> Pages { get; } = new();
        internal static List<Window> Windows { get; } = new();
        internal static LinkedList<Music> Musics { get; } = new();
        internal static List<EffectSound> Sounds { get; } = new();
        internal static Visibility BackgroundVis { get; set; } = Visibility.Collapsed;
        internal static ImageSource BackgroundImage { get; set; }
    }
}
