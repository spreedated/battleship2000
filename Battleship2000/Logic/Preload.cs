using Battleship2000.Views.Pages;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Battleship2000.Logic
{
    internal static class Preload
    {
        internal static event EventHandler<EventArgs> PreloadStep;
        internal static event EventHandler<EventArgs> PreloadComplete;

        internal static void Run()
        {
            Log.Information("[Preload] started");

            Task.Factory.StartNew(() =>
            {
                Views.MainWindow.Instance.Dispatcher.Invoke(() =>
                {
                    LoadPages();
                });
                PreloadStep?.Invoke(null, EventArgs.Empty);

                PreloadComplete?.Invoke(null, EventArgs.Empty);
            });
        }

        internal static void LoadPages()
        {
            Log.Information("[Preload] Loading pages...");
            ObjectStorage.pages.Add(new ConnectToServer());
            ObjectStorage.pages.Add(new MainMenu());
            ObjectStorage.pages.Add(new Playfield());
            ObjectStorage.pages.Add(new DedicatedServer());
            ObjectStorage.pages.Add(new Settings());
            ObjectStorage.pages.Add(new Settings_Player());
            ObjectStorage.pages.Add(new Settings_Network());
            Log.Information("[Preload] Loading pages finished");
        }
    }
}
