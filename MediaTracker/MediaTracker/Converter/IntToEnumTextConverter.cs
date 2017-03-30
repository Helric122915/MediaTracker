using MediaTracker.Helper;
using System;
using System.Windows.Data;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(int), typeof(string))]
    public class IntToEnumTextConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Returns ESRB as int.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((ESRB)((int)value)).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)((ESRB)value);
        }
    }
}