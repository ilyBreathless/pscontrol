using System;
using System.Globalization;

namespace PolarSectorsControl
{
    internal class Converter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse((parameter as string).Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double param);
            return param * (double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}