using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleship2000.ViewElements
{
    /// <summary>
    /// Interaction logic for SelectionArrow.xaml
    /// </summary>
    public partial class SelectionArrow : UserControl
    {
        public enum Rotations
        {
            Up,
            Down,
            Left,
            Right
        }

        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register("Rotation", typeof(Rotations), typeof(SelectionArrow), new PropertyMetadata(Rotations.Left, RotationPropertyChanged));
        public Rotations Rotation
        {
            get
            {
                return (Rotations)GetValue(RotationProperty);
            }

            set
            {
                SetValue(RotationProperty, value);
            }
        }

        protected static void RotationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Rotations r = (Rotations)e.NewValue;

            switch (r)
            {
                case Rotations.Up:
                    ((SelectionArrow)d).vBrush.Visual = (Visual)Application.Current.Resources["ArrowUp"];
                    break;
                case Rotations.Down:
                    ((SelectionArrow)d).vBrush.Visual = (Visual)Application.Current.Resources["ArrowDown"];
                    break;
                case Rotations.Left:
                    ((SelectionArrow)d).vBrush.Visual = (Visual)Application.Current.Resources["ArrowLeft"];
                    break;
                case Rotations.Right:
                    ((SelectionArrow)d).vBrush.Visual = (Visual)Application.Current.Resources["ArrowRight"];
                    break;
            }
        }

        public SelectionArrow()
        {
            InitializeComponent();

        }
    }
}
