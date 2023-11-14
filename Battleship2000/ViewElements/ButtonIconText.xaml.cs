using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewElements
{
    public partial class ButtonIconText : UserControl
    {
        public static readonly DependencyProperty VaadinIconProperty = DependencyProperty.Register("VaadinIcon", typeof(PackIconVaadinIconsKind), typeof(ButtonIconText), new FrameworkPropertyMetadata(PackIconVaadinIconsKind.None, new PropertyChangedCallback(OnIconChanged)));
        public PackIconVaadinIconsKind VaadinIcon
        {
            get => (PackIconVaadinIconsKind)this.GetValue(VaadinIconProperty);
            set => this.SetValue(VaadinIconProperty, value);
        }

        public static readonly DependencyProperty VaadinVisibilityProperty = DependencyProperty.Register("VaadinVisibility", typeof(Visibility), typeof(ButtonIconText), new FrameworkPropertyMetadata(Visibility.Collapsed));
        public Visibility VaadinVisibility
        {
            get => (Visibility)this.GetValue(VaadinVisibilityProperty);
            set => this.SetValue(VaadinVisibilityProperty, value);
        }

        public static readonly DependencyProperty ZondIconProperty = DependencyProperty.Register("ZondIcon", typeof(PackIconZondiconsKind), typeof(ButtonIconText), new FrameworkPropertyMetadata(PackIconZondiconsKind.None, new PropertyChangedCallback(OnIconChanged)));
        public PackIconZondiconsKind ZondIcon
        {
            get => (PackIconZondiconsKind)this.GetValue(ZondIconProperty);
            set => this.SetValue(ZondIconProperty, value);
        }

        public static readonly DependencyProperty ZondVisibilityProperty = DependencyProperty.Register("ZondVisibility", typeof(Visibility), typeof(ButtonIconText), new FrameworkPropertyMetadata(Visibility.Collapsed));
        public Visibility ZondVisibility
        {
            get => (Visibility)this.GetValue(ZondVisibilityProperty);
            set => this.SetValue(ZondVisibilityProperty, value);
        }

        public static readonly DependencyProperty ForkIconProperty = DependencyProperty.Register("ForkIcon", typeof(PackIconForkAwesomeKind), typeof(ButtonIconText), new FrameworkPropertyMetadata(PackIconForkAwesomeKind.None, new PropertyChangedCallback(OnIconChanged)));
        public PackIconForkAwesomeKind ForkIcon
        {
            get => (PackIconForkAwesomeKind)this.GetValue(ForkIconProperty);
            set => this.SetValue(ForkIconProperty, value);
        }

        public static readonly DependencyProperty ForkVisibilityProperty = DependencyProperty.Register("ForkVisibility", typeof(Visibility), typeof(ButtonIconText), new FrameworkPropertyMetadata(Visibility.Collapsed));
        public Visibility ForkVisibility
        {
            get => (Visibility)this.GetValue(ForkVisibilityProperty);
            set => this.SetValue(ForkVisibilityProperty, value);
        }

        public static readonly DependencyProperty MaterialIconProperty = DependencyProperty.Register("MaterialIcon", typeof(PackIconMaterialKind), typeof(ButtonIconText), new FrameworkPropertyMetadata(PackIconMaterialKind.None, new PropertyChangedCallback(OnIconChanged)));
        public PackIconMaterialKind MaterialIcon
        {
            get => (PackIconMaterialKind)this.GetValue(MaterialIconProperty);
            set => this.SetValue(MaterialIconProperty, value);
        }

        public static readonly DependencyProperty MaterialVisibilityProperty = DependencyProperty.Register("MaterialVisibility", typeof(Visibility), typeof(ButtonIconText), new FrameworkPropertyMetadata(Visibility.Collapsed));
        public Visibility MaterialVisibility
        {
            get => (Visibility)this.GetValue(MaterialVisibilityProperty);
            set => this.SetValue(MaterialVisibilityProperty, value);
        }

        public static readonly DependencyProperty EntypoIconProperty = DependencyProperty.Register("EntypoIcon", typeof(PackIconEntypoKind), typeof(ButtonIconText), new FrameworkPropertyMetadata(PackIconEntypoKind.None, new PropertyChangedCallback(OnIconChanged)));
        public PackIconEntypoKind EntypoIcon
        {
            get => (PackIconEntypoKind)this.GetValue(EntypoIconProperty);
            set => this.SetValue(EntypoIconProperty, value);
        }

        public static readonly DependencyProperty EntypoVisibilityProperty = DependencyProperty.Register("EntypoVisibility", typeof(Visibility), typeof(ButtonIconText), new FrameworkPropertyMetadata(Visibility.Collapsed));
        public Visibility EntypoVisibility
        {
            get => (Visibility)this.GetValue(EntypoVisibilityProperty);
            set => this.SetValue(EntypoVisibilityProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor", typeof(Brush), typeof(ButtonIconText), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        public Brush IconColor
        {
            get => (Brush)this.GetValue(IconColorProperty);
            set => this.SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register("TextColor", typeof(Brush), typeof(ButtonIconText), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        public Brush TextColor
        {
            get => (Brush)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ButtonIconText), new FrameworkPropertyMetadata(null));
        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ButtonIconText), new FrameworkPropertyMetadata(null));
        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ButtonIconText), new FrameworkPropertyMetadata(null));
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonIconText v = (ButtonIconText)d;

            if (v != null)
            {
                v.MaterialVisibility = v.MaterialIcon == PackIconMaterialKind.None ? Visibility.Collapsed : Visibility.Visible;
                v.ZondVisibility = v.ZondIcon == PackIconZondiconsKind.None ? Visibility.Collapsed : Visibility.Visible;
                v.ForkVisibility = v.ForkIcon == PackIconForkAwesomeKind.None ? Visibility.Collapsed : Visibility.Visible;
                v.EntypoVisibility = v.EntypoIcon == PackIconEntypoKind.None ? Visibility.Collapsed : Visibility.Visible;
                v.VaadinVisibility = v.VaadinIcon == PackIconVaadinIconsKind.None ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public ButtonIconText()
        {
            this.InitializeComponent();
            this.IsEnabledChanged += this.IsEnabled_Changed;
        }

        private Brush privateIconColor;
        private Brush privateTextColor;
        private void SetColor()
        {
            if (this.IsEnabled)
            {
                this.IconColor = privateIconColor;
                this.TextColor = privateTextColor;
            }
            else
            {
                privateIconColor = this.IconColor;
                privateTextColor = this.TextColor;
                this.IconColor = new SolidColorBrush(Color.FromRgb(34, 34, 34));
                this.TextColor = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            }
        }

        private void IsEnabled_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetColor();
        }
    }
}
