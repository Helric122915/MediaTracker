using MediaTracker.Helper;
using System;
using System.Windows.Data;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(VideoGameSort), typeof(int))]
    public class VideoGameSortToSelectedIndex : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Returns VideoGameSort as int.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)((VideoGameSort)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (VideoGameSort)((int)value);
        }
    }
}
