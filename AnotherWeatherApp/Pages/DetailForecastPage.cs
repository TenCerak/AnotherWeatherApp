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
using CommunityToolkit.Maui.Views;
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
                            new Button { Text = "Get current weather", Command = new Command( () => BindingContext.LoadDataAsync().ConfigureAwait(true)) },
                            new Label { Text = "Current Weather" },


                            new CurrentWeatherContentView(BindingContext).Padding(new(20)),

                            new ChartView
                                {
                                    Series =
                                    {
                                        new LineSeries
                                        {
                                            Data = new SeriesDataAdapter
                                            {
                                                ArgumentDataMember = "Time",
                                                AllowLiveDataUpdates = true,
                                            }.Assign(out SeriesDataAdapter dataAdapter)
                                            .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts)                                            ,
                                            Style = new LineSeriesStyle
                                            {
                                                Stroke = Colors.Orange,
                                                MarkerStyle = new MarkerStyle
                                                {
                                                    Fill = Colors.Orange,
                                                    Stroke = Colors.White,
                                                }
                                            }  ,
                                            Label = new MarkerSeriesLabel
                                            {
                                                Angle = 45,
                                                TextPattern = "{V}",
                                                Indent = 10

                                            },
                                            DisplayName = "Temperature"

                                        },
                                        new BarSeries
                                        {
                                            Data = new SeriesDataAdapter
                                            {
                                                ArgumentDataMember = "Time",
                                                AllowLiveDataUpdates = true,
                                            }.Assign(out SeriesDataAdapter dataAdapterRain)
                                            .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts)                                            ,
                                            Style = new BarSeriesStyle
                                            {
                                                Stroke = Colors.Blue,
                                            },
                                            DisplayName = "Rain"
                                        },
                                        new BarSeries
                                        {
                                            Data = new SeriesDataAdapter
                                            {
                                                ArgumentDataMember = "Time",
                                                AllowLiveDataUpdates = true,
                                            }.Assign(out SeriesDataAdapter dataAdapterSnow)
                                            .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts)                                            ,
                                            Style = new BarSeriesStyle
                                            {
                                                Stroke = Colors.Blue,
                                            },
                                            DisplayName = "Snow"
                                        }

                                    }
                                }.Row(0)
                                .Height(300),
                            //AddChartView()
                            //.Height(300),
                            new Expander()
                            {
                                IsExpanded = true,
                                Content = new StackLayout()
                                {
                                    new Label().Bind<Label, DetailForecastViewModel, CurrentWeather, string>(Label.TextProperty,
                                    getter: static (DetailForecastViewModel vm) => vm.CurrentWeather, converter: StringConverter)
                                    .Padding(20),

                                    new Label()
                                    .Text("---------------------------------------------"),

                                    new Label().Bind<Label, DetailForecastViewModel, HourlyForecastResponse, string>(Label.TextProperty,
                                    getter: static (DetailForecastViewModel vm) => vm.Forecast, converter: StringConverter)
                                    .Padding(20),
                                }
                            },


                             

                        }

                    }

                };

            dataAdapter.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.Temperature), Type = DevExpress.Maui.Charts.ValueType.Value });
            dataAdapterRain.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.RainPrecipitation), Type = DevExpress.Maui.Charts.ValueType.Value });
            dataAdapterSnow.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.SnowPrecipitation), Type = DevExpress.Maui.Charts.ValueType.Value });
        }

        ChartView AddChartView()
        {

            var chartView = new ChartView();

            var series = new LineSeries();
            SeriesDataAdapter dataAdapter = new SeriesDataAdapter();
            dataAdapter.Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts);
            dataAdapter.AllowLiveDataUpdates = true;
            dataAdapter.ArgumentDataMember = nameof(HourlyForecast.Time);

            

            ValueDataMember valueDataMember = new ValueDataMember();
            valueDataMember.Member = nameof(HourlyForecast.Temperature);
            valueDataMember.Type = DevExpress.Maui.Charts.ValueType.Value;
            valueDataMember.Bind(ValueDataMember.BindingContextProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts);

            dataAdapter.ValueDataMembers.Add(valueDataMember);
            series.Data = dataAdapter;
            chartView.Series.Add(series);
            chartView.Bind(BindingContextProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts);
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
