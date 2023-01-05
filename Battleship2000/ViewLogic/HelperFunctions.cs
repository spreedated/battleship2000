using Battleship2000.Logic;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Controls;
using Serilog;

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
                    foreach (DictionaryEntry d in r)
                    {
                        if (d.Key.ToString().ToLower().EndsWith("baml"))
                        {
                            string xx = (string)d.Key;
                            xx = xx.Substring(0, xx.LastIndexOf('.')) + ".xaml";

                            q.Add(xx.ToLower());
                        }
                    }
                }
            }

            if (q.Any(x => x.Contains(xaml.ToLower())))
            {
                return "pack://application:,,,/" + q.First(x => x.Contains(xaml.ToLower()));
            }

            return null;
        }

        public static void NavigateMainframeTo(string pagename)
        {
            Page p = ObjectStorage.pages.FirstOrDefault(x => x.GetType().Name.ToLower().Contains(pagename.ToLower()));

            if (p == null)
            {
                Log.Warning($"[HelperFunctions][NavigateMainframeTo] Cannot find page \"{pagename}\"");
                return;
            }

            Views.MainWindow.Instance.MainFrame.Navigate(p);

            Log.Information($"[HelperFunctions][NavigateMainframeTo] Navigated to \"{pagename}\" page");
        }

        public static void NavigateSettingsframeTo(string pagename)
        {
            Page p = ObjectStorage.pages.FirstOrDefault(x => x.GetType().Name.ToLower().Contains(pagename.ToLower()));

            if (p == null)
            {
                Log.Warning($"[HelperFunctions][NavigateSettingsframeTo] Cannot find page \"{pagename}\"");
                return;
            }

            Views.Pages.Settings.Instance.SettingsFrame.Navigate(p);

            Log.Information($"[HelperFunctions][NavigateSettingsframeTo] Navigated to \"{pagename}\" page");
        }
    }
}
