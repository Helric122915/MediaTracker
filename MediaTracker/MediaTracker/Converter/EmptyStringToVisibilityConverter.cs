using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class EmptyStringToVisibilityConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// If the string is empty Hidden will be returned. Otherwise, Visible will be returned.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((string)value != "")
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
