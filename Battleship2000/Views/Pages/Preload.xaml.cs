using Battleship2000.ViewLogic;
using Battleship2000.ViewModels;
using Serilog;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Preload.xaml
    /// </summary>
    public partial class Preload : Page
    {
        internal static PreloadViewModel Vm { get; private set; }
        public Preload()
        {
            InitializeComponent();
            Vm = (PreloadViewModel)this.DataContext;
            Log.Verbose("[Preload] Page loaded");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Logic.Preload.PreloadStep += PreloadStepped;
            Logic.Preload.PreloadComplete += PreloadComplete;

            Logic.Preload.Run();
        }

        private void PreloadStepped(object sender, EventArgs e)
        {
            Vm.ProgressbarValue += 10.0d;
            Log.Verbose($"[PreloadStepped] Step \"{Vm.ProgressbarValue}\"");
#if DEBUG
            Thread.Sleep(1000);
#endif
        }

        private void PreloadComplete(object sender, EventArgs e)
        {
            Vm.ProgressbarValue = 100.0d;
            Vm.LoadingText = "Complete !";
            Log.Information("[PreloadComplete] Loading completed");

            Thread.Sleep(1250);

            this.Dispatcher.Invoke(() =>
            {
                HelperFunctions.NavigateMainframeTo("mainmenu");
                Log.Verbose("[PreloadComplete] Navigating to main menu");
            });
        }
    }
}
