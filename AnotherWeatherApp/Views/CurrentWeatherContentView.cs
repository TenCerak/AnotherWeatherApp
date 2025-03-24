using AnotherWeatherApp.Models;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Markup.LeftToRight;
using System.Globalization;
using System.Text.Json;

namespace AnotherWeatherApp.Views;

public class CurrentWeatherContentView : ContentView
{
    public class IconConverter : IValueConverter
    {
        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string icon)
            {
                return $"http://openweathermap.org/img/w/{icon}.png";
            }
            return null;
        }

        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public CurrentWeatherContentView(in DetailForecastViewModel model)
    {
        Content = new VerticalStackLayout
        {
            Children =
            {
                new HorizontalStackLayout
                {
                    new Image()
                 .Bind(Image.SourceProperty,
                    getter: static (DetailForecastViewModel vm) => vm.IconSource)

                 .Size(100),

                new VerticalStackLayout{
                 new Label()
                 .Bind(Label.TextProperty,
                    getter: static (DetailForecastViewModel vm) => vm.CurrentWeather.main.temp,
                    handlers:
                    [
                        (vm => vm, nameof(DetailForecastViewModel.CurrentWeather)),
                        (vm => vm.CurrentWeather, nameof(DetailForecastViewModel.CurrentWeather.main.temp)),
                    ],
                    stringFormat: Properties.Resources.TemperatureMetricFormatString),

                 new Label()
                 .Bind(Label.TextProperty,
                    getter: static (DetailForecastViewModel vm) => vm.CurrentWeather.main.temp_min,
                    handlers:
                    [
                        (vm => vm, nameof(DetailForecastViewModel.CurrentWeather)),
                        (vm => vm.CurrentWeather, nameof(DetailForecastViewModel.CurrentWeather.main.temp_min)),
                    ],
                    stringFormat: Properties.Resources.TemperatureMinMetricFormatString),

                 new Label()
                 .Bind(Label.TextProperty,
                    getter: static (DetailForecastViewModel vm) => vm.CurrentWeather.main.temp_max,
                    handlers:
                        [
                            (vm => vm, nameof(DetailForecastViewModel.CurrentWeather)),
                            (vm => vm.CurrentWeather, nameof(DetailForecastViewModel.CurrentWeather.main.temp_max)),
                        ],
                    stringFormat: Properties.Resources.TemperatureMaxMetricFormatString),
                 new Label()
                 .Bind(Label.TextProperty,
                 getter: static (DetailForecastViewModel vm) => vm.Description)
                }

                }
            }
        };

    }
}