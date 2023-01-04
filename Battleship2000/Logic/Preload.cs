using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Battleship2000.Views;
using Battleship2000.Views.Pages;

namespace Battleship2000.Logic
{
    internal static class Preload
    {
        internal static event EventHandler<EventArgs> PreloadStep;
        internal static event EventHandler<EventArgs> PreloadComplete;

        internal static void Run()
        {
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
            ObjectStorage.pages.Add(new ConnectToServer());
            ObjectStorage.pages.Add(new MainMenu());
            ObjectStorage.pages.Add(new Playfield());
        }
    }
}
