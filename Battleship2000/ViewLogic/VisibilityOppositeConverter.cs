using neXn.Lib.Wpf.ViewLogic;
using System;
using System.Globalization;
using System.Windows;

namespace Battleship2000.ViewLogic
{
    internal class VisibilityOppositeConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility b = (Visibility)value;

            switch (b)
            {
                case Visibility.Visible:
                    return Visibility.Hidden;
                default:
                    return Visibility.Visible;
            }
        }
    }
}
