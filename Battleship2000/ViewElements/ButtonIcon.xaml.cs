using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewElements
{
    public partial class ButtonIcon : UserControl
    {
        public static readonly DependencyProperty VaadinIconProperty = DependencyProperty.Register("VaadinIcon", typeof(PackIconVaadinIconsKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconVaadinIconsKind.None));
        public PackIconVaadinIconsKind VaadinIcon
        {
            get => (PackIconVaadinIconsKind)this.GetValue(VaadinIconProperty);
            set => this.SetValue(VaadinIconProperty, value);
        }

        public static readonly DependencyProperty ZondIconProperty = DependencyProperty.Register("ZondIcon", typeof(PackIconZondiconsKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconZondiconsKind.None));
        public PackIconZondiconsKind ZondIcon
        {
            get => (PackIconZondiconsKind)this.GetValue(ZondIconProperty);
            set => this.SetValue(ZondIconProperty, value);
        }

        public static readonly DependencyProperty ForkIconProperty = DependencyProperty.Register("ForkIcon", typeof(PackIconForkAwesomeKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconForkAwesomeKind.None));
        public PackIconForkAwesomeKind ForkIcon
        {
            get => (PackIconForkAwesomeKind)this.GetValue(ForkIconProperty);
            set => this.SetValue(ForkIconProperty, value);
        }

        public static readonly DependencyProperty MaterialIconProperty = DependencyProperty.Register("MaterialIcon", typeof(PackIconMaterialKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconMaterialKind.None));
        public PackIconMaterialKind MaterialIcon
        {
            get => (PackIconMaterialKind)this.GetValue(MaterialIconProperty);
            set => this.SetValue(MaterialIconProperty, value);
        }

        public static readonly DependencyProperty EntypoIconProperty = DependencyProperty.Register("EntypoIcon", typeof(PackIconEntypoKind), typeof(ButtonIcon), new FrameworkPropertyMetadata(PackIconEntypoKind.None));
        public PackIconEntypoKind EntypoIcon
        {
            get => (PackIconEntypoKind)this.GetValue(EntypoIconProperty);
            set => this.SetValue(EntypoIconProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor", typeof(Brush), typeof(ButtonIcon), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        public Brush IconColor
        {
            get => (Brush)this.GetValue(IconColorProperty);
            set => this.SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ButtonIcon), new FrameworkPropertyMetadata(null));
        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ButtonIcon), new FrameworkPropertyMetadata(null));
        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public ButtonIcon()
        {
            this.InitializeComponent();
            this.IsEnabledChanged += this.IsEnabled_Changed;
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
