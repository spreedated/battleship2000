using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Battleship2000.ViewLogic
{
    internal class FloatToPercentAudioSliderConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float b = (float)value;

            if (b <= 0.0f)
            {
                return null;
            }

            if (b >= 1.0f)
            {
                return null;
            }

            return (b * 100).ToString("0.00") + " %";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
