using MediaTracker.Helper;
using System;
using System.Windows;
using System.Windows.Data;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(ESRB), typeof(Visibility))]
    public class EnumToVisibilityConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// If the ESRB rating is None Hidden will be returned. Otherwise, Visible will be returned.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((ESRB)value != ESRB.None)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
