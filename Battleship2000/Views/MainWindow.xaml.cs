using Battleship2000.Logic;
using Battleship2000.ViewModels;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Battleship2000.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public static MainWindowViewModel InstanceVM { get; private set; }
        public MainWindow()
        {
            this.InitializeComponent();
            Instance = this;
            InstanceVM = (MainWindowViewModel)this.DataContext;

            if (RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize != default)
            {
                this.Width = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize.Width;
                this.Height = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize.Height;
            }

            RefreshBackground();
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
                    InstanceVM.BackgroundVis = Visibility.Hidden;
                    RuntimeStorage.BackgroundVis = Visibility.Hidden;
                    break;
                case "blue":
                    InstanceVM.BackgroundVis = Visibility.Visible;
                    InstanceVM.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                    RuntimeStorage.BackgroundVis = Visibility.Visible;
                    RuntimeStorage.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                    break;
            }
        }
    }
}
