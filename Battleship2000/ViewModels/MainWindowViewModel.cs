using Battleship2000.Logic;
using Battleship2000.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Battleship2000.Logic.Constants;
using static Battleship2000.Logic.RuntimeStorage;

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

        public string WindowTitle { get; } = $"{MyAssembly.GetCustomAttribute<AssemblyTitleAttribute>().Title} v{MyAssembly.GetName().Version}";
    }
}
