#pragma warning disable CA1845
#pragma warning disable IDE0057

using Battleship2000.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Media.Imaging;
using System.Windows;
using Battleship2000.Views;

namespace Battleship2000.ViewLogic
{
    internal static class HelperFunctions
    {
        public static string GetXamlPath(string xaml)
        {
            List<string> q = new();

            using (Stream ss = typeof(HelperFunctions).Assembly.GetManifestResourceStream(typeof(HelperFunctions).Assembly.GetName().Name + ".g.resources"))
            {
                using (ResourceReader r = new(ss))
                {
                    foreach (DictionaryEntry d in r.OfType<DictionaryEntry>().Where(x => x.Key.ToString().ToLower().EndsWith("baml")))
                    {
                        string xx = (string)d.Key;
                        xx = xx.Substring(0, xx.LastIndexOf('.')) + ".xaml";

                        q.Add(xx.ToLower());
                    }
                }
            }

            if (q.Any(x => x.Contains(xaml.ToLower())))
            {
                return "pack://application:,,,/" + q.First(x => x.Contains(xaml.ToLower()));
            }

            return null;
        }

        public static void RefreshBackground()
        {
            if (RuntimeStorage.BackgroundImage == null)
            {
                RuntimeStorage.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                RuntimeStorage.BackgroundVis = Visibility.Collapsed;
            }

            switch (RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Visual.Background.ToLower())
            {
                case "oldschool":
                    MainWindow.InstanceVM.BackgroundVis = Visibility.Hidden;
                    RuntimeStorage.BackgroundVis = Visibility.Hidden;
                    break;
                case "blue":
                    MainWindow.InstanceVM.BackgroundVis = Visibility.Visible;
                    MainWindow.InstanceVM.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                    RuntimeStorage.BackgroundVis = Visibility.Visible;
                    RuntimeStorage.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                    break;
            }
        }
    }
}
