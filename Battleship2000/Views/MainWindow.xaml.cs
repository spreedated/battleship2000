using Battleship2000.ViewModels;
using System.Windows;

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
        }
    }
}
