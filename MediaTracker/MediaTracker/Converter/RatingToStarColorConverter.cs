using System;
using System.Windows.Data;

namespace MediaTracker.Converter
{
    [ValueConversion(typeof(ushort), typeof(bool))]
    public class RatingToStarColorConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// If the value is greater than or equal to the parameter then return true, otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ushort val = (ushort)value;
            //ushort param = (ushort)parameter;
            ushort param = ushort.Parse((string)parameter);

            if (val >= param)
                return true;
            else
                return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
