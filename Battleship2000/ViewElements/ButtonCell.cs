using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleship2000.ViewElements
{
    public class ButtonCell : Button
    {
        public static readonly DependencyProperty BackgroundCellProperty = DependencyProperty.Register("BackgroundCell", typeof(Brush), typeof(ButtonCell), new PropertyMetadata(Brushes.Gray));

        public Brush BackgroundCell
        {
            get => (Brush)this.GetValue(BackgroundCellProperty);
            set => this.SetValue(BackgroundCellProperty, value);
        }
    }
}
