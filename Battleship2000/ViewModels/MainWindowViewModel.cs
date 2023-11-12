using Battleship2000.Logic;
using Battleship2000.Views;
using Serilog;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using static Battleship2000.Logic.Constants;

namespace Battleship2000.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public ICommand ResizeCommand { get; } = new RelayCommand<SizeChangedEventArgs>((o) =>
        {
            RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize = o.NewSize;
        });

        private ImageSource _BackgroundImage = new BitmapImage(new Uri(URI_BACKGROUND_BLUE));
        public ImageSource BackgroundImage
        {
            get => this._BackgroundImage;
            set => base.SetProperty<ImageSource>(ref this._BackgroundImage, value);
        }

        private Visibility _BackgroundVis = Visibility.Hidden;
        public Visibility BackgroundVis
        {
            get => this._BackgroundVis;
            set => base.SetProperty<Visibility>(ref this._BackgroundVis, value);
        }

        private MainWindow _Instance;
        public MainWindow Instance
        {
            get => this._Instance;
            set => base.SetProperty<MainWindow>(ref this._Instance, value);
        }

        private Page _CurrentFramePage;
        public Page CurrentFramePage
        {
            get => this._CurrentFramePage;
            set => base.SetProperty<Page>(ref this._CurrentFramePage, value);
        }

        public string WindowTitle { get; } = $"{typeof(MainWindowViewModel).Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title} v{typeof(MainWindowViewModel).Assembly.GetName().Version}";

        public static void Navigate(string pagename)
        {
            Page p = RuntimeStorage.Pages.Find(x => x.GetType().Name.ToLower().Contains(pagename.ToLower()));

            if (p == null)
            {
                Log.Warning($"Cannot find page \"{pagename}\"");
                return;
            }

            ((MainWindowViewModel)Application.Current.MainWindow.DataContext).CurrentFramePage = p;

            Log.Information($"Navigated to \"{pagename}\" page");
        }
    }
}
