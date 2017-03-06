using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(string), typeof(double))]
    public class EmptyStringToHeightConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// If the string is empty NaN (auto) will be returned. Otherwise, 0 will be returned.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((string)value != "")
                return double.NaN;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
