using Battleship2000.Logic;
using Battleship2000.ViewModels;
using Battleship2000.Views;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static Battleship2000.Logic.Constants;
using static Battleship2000.Logic.Globals;

namespace Battleship2000.ViewLogic
{
    internal static class HelperFunctions
    {
        public static string GetXamlPath(string xaml)
        {
            List<string> q = new();

            using (Stream ss = MyAssembly.GetManifestResourceStream(MyAssembly.GetName().Name + ".g.resources"))
            {
                using (ResourceReader r = new(ss))
                {
                    foreach (DictionaryEntry d in r.OfType<DictionaryEntry>().Where(x => x.Key.ToString().ToLower().EndsWith("baml")))
                    {
                        string xx = (string)d.Key;
                        xx = string.Concat(xx.AsSpan(0, xx.LastIndexOf('.')), ".xaml");

                        q.Add(xx.ToLower());
                    }
                }
            }

            if (q.Exists(x => x.Contains(xaml.ToLower(), StringComparison.InvariantCulture)))
            {
                return "pack://application:,,,/" + q.First(x => x.Contains(xaml.ToLower(), StringComparison.InvariantCulture));
            }

            return null;
        }

        public static void RefreshBackground()
        {
            if (Globals.BackgroundImage == null)
            {
                Globals.BackgroundImage = new BitmapImage(new Uri(URI_BACKGROUND_BLUE));
                Globals.BackgroundVis = Visibility.Collapsed;
            }

            switch (Globals.ConfigurationHandler.RuntimeConfiguration.Visual.Background.ToLower())
            {
                case "oldschool":
                    MainWindow.InstanceVM.BackgroundVis = Visibility.Hidden;
                    Globals.BackgroundVis = Visibility.Hidden;
                    break;
                case "blue":
                    MainWindow.InstanceVM.BackgroundVis = Visibility.Visible;
                    MainWindow.InstanceVM.BackgroundImage = new BitmapImage(new Uri(URI_BACKGROUND_BLUE));
                    Globals.BackgroundVis = Visibility.Visible;
                    Globals.BackgroundImage = new BitmapImage(new Uri(URI_BACKGROUND_BLUE));
                    break;
            }
        }

        public static void Navigate(string pagename)
        {
            Page p = Globals.Pages.Find(x => x.GetType().Name.ToLower().Contains(pagename.ToLower(), StringComparison.InvariantCulture));

            if (p == null)
            {
                Log.Warning($"Cannot find page \"{pagename}\"");
                return;
            }

            ((MainWindowViewModel)Application.Current.MainWindow.DataContext).CurrentFramePage = p;

            Log.Information($"Navigated to \"{pagename}\" page");
        }
    }
}
