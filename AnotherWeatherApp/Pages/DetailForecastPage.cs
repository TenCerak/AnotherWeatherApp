using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Models;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.Views;
using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DevExpress.Maui.Charts;

namespace AnotherWeatherApp.Pages
{
    public class DetailForecastPage : BaseContentPage<DetailForecastViewModel>
    {
        StringConverter StringConverter = new StringConverter();
        public DetailForecastPage(IAnalyticsService analyticsService, DetailForecastViewModel viewModel) : base(viewModel, analyticsService)
        {
            
            Content =
                new ScrollView()
                {
                    Content =
                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = "Detail Forecast" },
                            new Button { Text = "Get current weather", Command = new Command(async () => await BindingContext.LoadDataAsync().ConfigureAwait(true)) },
                            new Label { Text = "Current Weather" },


                            new CurrentWeatherContentView(BindingContext).Padding(new(20)),
                            AddChartView(),

                            new Label().Bind<Label, DetailForecastViewModel, CurrentWeather, string>(Label.TextProperty,
                            getter: static (DetailForecastViewModel vm) => vm.CurrentWeather, converter: StringConverter)
                            .Padding(20),

                            new Label()
                            .Text("---------------------------------------------"),

                            new Label().Bind<Label, DetailForecastViewModel, HourlyForecast, string>(Label.TextProperty,
                            getter: static (DetailForecastViewModel vm) => vm.Forecast, converter: StringConverter)
                            .Padding(20),

                        }

                    }

                };
        }

        ChartView AddChartView()
        {

            var chartView = new ChartView();
            
            var series = new LineSeries();
            var source = new HourlyWeatherDataChartAdapter();
            source.HourlyForecast = BindingContext.Forecast;
            series.Bind(BindingContextProperty, static (DetailForecastViewModel vm) => vm.Forecast);




            chartView.Bind(BindingContextProperty,static (DetailForecastViewModel vm) => vm.Forecast);
            return chartView;
        }
        
    }

    


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
