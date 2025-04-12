using System;
using System.Globalization;
using System.Linq;

namespace AnotherWeatherApp.Converters
{
    public class LocationToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCurrent && isCurrent)
                return Colors.Orange;
            return Colors.DarkGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
