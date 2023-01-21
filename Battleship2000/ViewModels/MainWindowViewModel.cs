#pragma warning disable S1075

using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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

        public static void Navigate(string pagename)
        {
            Page p = ObjectStorage.pages.FirstOrDefault(x => x.GetType().Name.ToLower().Contains(pagename.ToLower()));

            if (p == null)
            {
                Log.Warning($"Cannot find page \"{pagename}\"");
                return;
            }

            ((MainWindowViewModel)Application.Current.MainWindow.DataContext).CurrentFramePage = p;

            Log.Information($"Navigated to \"{pagename}\" page");
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

        private MainWindow _Instance;
        public MainWindow Instance
        {
            get
            {
                return this._Instance;
            }
            set
            {
                this._Instance = value;
                base.OnPropertyChanged(nameof(this.Instance));
            }
        }

        private Page _CurrentFramePage;
        public Page CurrentFramePage
        {
            get
            {
                return this._CurrentFramePage;
            }
            set
            {
                this._CurrentFramePage = value;
                base.OnPropertyChanged(nameof(this.CurrentFramePage));
            }
        }
    }
}
