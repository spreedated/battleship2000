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
            InitializeComponent();
            Instance = this;
            InstanceVM = (MainWindowViewModel)this.DataContext;

            this.RefreshBackground();
        }

        public void RefreshBackground()
        {
            if (RuntimeStorage.BackgroundImage == null)
            {
                RuntimeStorage.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                RuntimeStorage.BackgroundVis = Visibility.Collapsed;
            }

            switch (RuntimeStorage.Config.Visual.Background.ToLower())
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
