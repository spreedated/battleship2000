﻿using Battleship2000.ViewLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship2000.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string WindowTitle { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title} v{typeof(MainWindowViewModel).Assembly.GetName().Version}";

        private Uri _FrameSource = new Uri("pack://application:,,,/views/pages/MainMenu.xaml");
        public Uri FrameSource
        {
            get
            {
                return _FrameSource;
            }
            set
            {
                _FrameSource = value;
                base.OnPropertyChanged(nameof(FrameSource));
            }
        }

        private ImageSource _BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/battleship1-1280x736.png"));
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
