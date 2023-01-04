using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
#if DEBUG
            Thread.Sleep(1000);
#endif
        }

        private void PreloadComplete(object sender, EventArgs e)
        {
            Vm.ProgressbarValue = 100.0d;
            Vm.LoadingText = "Complete !";

            Thread.Sleep(1250);

            this.Dispatcher.Invoke(() =>
            {
                HelperFunctions.NavigateMainframeTo("mainmenu");
            });
        }
    }
}
