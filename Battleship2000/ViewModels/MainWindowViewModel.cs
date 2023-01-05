using Battleship2000.ViewLogic;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship2000.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string WindowTitle { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title} v{typeof(MainWindowViewModel).Assembly.GetName().Version}";

        private ImageSource _BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/blue.png"));
        public ImageSource BackgroundImage
        {
            get
            {
                return _BackgroundImage;
            }
            set
            {
                _BackgroundImage = value;
                base.OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        private Visibility _BackgroundVis = Visibility.Hidden;
        public Visibility BackgroundVis
        {
            get
            {
                return _BackgroundVis;
            }
            set
            {
                _BackgroundVis = value;
                base.OnPropertyChanged(nameof(BackgroundVis));
            }
        }
    }
}
