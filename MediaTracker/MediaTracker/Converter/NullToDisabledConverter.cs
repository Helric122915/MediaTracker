using MediaTracker.Helper;
using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(BoxOfficeMovie), typeof(bool))]
    public class NullToDiabledConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// If the object is not null return true. Otherwise, return false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
