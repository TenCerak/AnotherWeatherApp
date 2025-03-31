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
using Microsoft.Maui.Controls;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using Microsoft.Maui.Layouts;

namespace AnotherWeatherApp.Pages
{
    public class DetailForecastPage : BaseContentPage<DetailForecastViewModel>
    {
        StringConverter StringConverter = new StringConverter();
        public DetailForecastPage(IAnalyticsService analyticsService, DetailForecastViewModel viewModel) : base(viewModel, analyticsService)
        {
            this.Title = Properties.Resources.DetailForecastTitle;
            Content = new RefreshView()
            {
                Command = new Command(async () => await BindingContext.LoadDataAsync().ConfigureAwait(true)),
                Content = new ScrollView()
                {

                    Content = new FlexLayout()
                    {
                        JustifyContent = FlexJustify.SpaceBetween,
                        AlignContent = FlexAlignContent.Start,
                        Direction = FlexDirection.Column,
                        HeightRequest = 700,
                        Children = {
                            new CurrentWeatherContentView(viewModel),
                            new BasicHourlyForecastChartView(viewModel)
                        }

                    }

                }
            }
            .Bind(RefreshView.IsRefreshingProperty, static (DetailForecastViewModel vm) => vm.IsLoading);

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
