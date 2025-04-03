using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace AnotherWeatherApp.Converters
{
    public class StringConverter : IValueConverter
    {
        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return JsonSerializer.Serialize(value, options: new() { WriteIndented = true });
        }

        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
