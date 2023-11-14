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
            this.InitializeComponent();
            Vm = (PreloadViewModel)this.DataContext;
            Log.Verbose("Page loaded");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Logic.Preload.PreloadStep += this.PreloadStepped;
            Logic.Preload.PreloadComplete += this.PreloadComplete;

            Logic.Preload.Run();
        }

        private void PreloadStepped(object sender, EventArgs e)
        {
            Vm.ProgressbarValue += 10.0d;
            Log.Verbose($"Step \"{Vm.ProgressbarValue}\"");
        }

        private void PreloadComplete(object sender, EventArgs e)
        {
            Vm.ProgressbarValue = 100.0d;
            Vm.LoadingText = "Loading Complete";
            Log.Information("Loading completed");

            Thread.Sleep(0);
#if !DEBUG
            Thread.Sleep(1250);
#endif

            this.Dispatcher.Invoke(() =>
            {
                ViewLogic.HelperFunctions.Navigate("mainmenu");
                Log.Verbose("Navigating to main menu");
            });
        }
    }
}
