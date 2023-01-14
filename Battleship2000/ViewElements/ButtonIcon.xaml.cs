using MahApps.Metro.IconPacks;
using Serilog;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewElements
{
    /// <summary>
    /// Interaction logic for ButtonIcon.xaml
    /// </summary>
    public partial class ButtonIcon : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static readonly DependencyProperty ZondIconProperty = DependencyProperty.Register("ZondIcon", typeof(PackIconZondiconsKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconZondiconsKind.None));
        public PackIconZondiconsKind ZondIcon
        {
            get => (PackIconZondiconsKind)GetValue(ZondIconProperty);
            set => SetValue(ZondIconProperty, value);
        }

        public static readonly DependencyProperty ForkIconProperty = DependencyProperty.Register("ForkIcon", typeof(PackIconForkAwesomeKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconForkAwesomeKind.None));
        public PackIconForkAwesomeKind ForkIcon
        {
            get => (PackIconForkAwesomeKind)GetValue(ForkIconProperty);
            set => SetValue(ForkIconProperty, value);
        }

        public static readonly DependencyProperty MaterialIconProperty = DependencyProperty.Register("MaterialIcon", typeof(PackIconMaterialKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconMaterialKind.None));
        public PackIconMaterialKind MaterialIcon
        {
            get => (PackIconMaterialKind)GetValue(MaterialIconProperty);
            set => SetValue(MaterialIconProperty, value);
        }

        public static readonly DependencyProperty EntypoIconProperty = DependencyProperty.Register("EntypoIcon", typeof(PackIconEntypoKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconEntypoKind.None));
        public PackIconEntypoKind EntypoIcon
        {
            get => (PackIconEntypoKind)GetValue(EntypoIconProperty);
            set => SetValue(EntypoIconProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor", typeof(Brush), typeof(ButtonIcon), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ButtonIcon), new FrameworkPropertyMetadata(null));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ButtonIcon()
        {
            InitializeComponent();
            this.IsEnabledChanged += IsEnabled_Changed;
        }

        private Brush privateColor;
        private void SetColor()
        {
            if (this.IsEnabled)
            {
                this.IconColor = privateColor;
            }
            else
            {
                privateColor = this.IconColor;
                this.IconColor = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            }
        }

        private void IsEnabled_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetColor();
        }
    }
}
