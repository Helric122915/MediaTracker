using MediaTracker.Helper;
using System;
using System.Windows.Data;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(MovieSort), typeof(int))]
    public class MovieSortToSelectedIndex : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Returns MovieSort as int.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)((MovieSort)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (MovieSort)((int)value);
        }
    }
}
