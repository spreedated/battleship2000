using neXn.Lib.Wpf.ViewLogic;
using System;
using System.Globalization;

namespace Battleship2000.ViewLogic
{
    internal class FloatToPercentAudioSliderConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
    }
}
