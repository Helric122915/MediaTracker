using MediaTracker.Helper;
using System;
using System.Windows.Data;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(MPAA), typeof(int))]
    public class MpaaToSelectedIndex : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Returns MPAA as int.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)((MPAA)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (MPAA)((int)value);
        }
    }
}