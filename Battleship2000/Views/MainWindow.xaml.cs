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
            switch (ObjectStorage.Config.Appearance.Background.ToLower())
            {
                case "oldschool":
                    InstanceVM.BackgroundVis = Visibility.Hidden;
                    ObjectStorage.BackgroundVis = Visibility.Hidden;
                    break;
                case "blue":
                    InstanceVM.BackgroundVis = Visibility.Visible;
                    InstanceVM.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                    ObjectStorage.BackgroundVis = Visibility.Visible;
                    ObjectStorage.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
                    break;
            }
        }
    }
}
