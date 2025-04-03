using AnotherWeatherApp.Models;
using CommunityToolkit.Maui.Markup;
using DevExpress.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Views
{
    public class WindHourlyForecastChartView : ContentView
    {
        public WindHourlyForecastChartView(in DetailForecastViewModel model)
        {
            Content = new ChartView
            {
                Hint = new Hint()
                {
                    Behavior = new TooltipHintBehavior()
                    {
                        ShowPointTooltip = true,
                        ShowSeriesTooltip = false,
                    }
                },

                Series =
                {
                    new SplineSeries
                    {
                        Data = new SeriesDataAdapter
                        {
                            ArgumentDataMember = "Time",
                            AllowLiveDataUpdates = false,
                        }.Assign(out SeriesDataAdapter dataAdapter)
                        .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts),
                        Style = new LineSeriesStyle
                        {
                            Stroke = Colors.AliceBlue,
                            MarkerStyle = new MarkerStyle
                            {
                                Fill = Colors.AliceBlue,
                                Stroke = Colors.AliceBlue,
                            },

                        },
                        DisplayName = "Wind speed",
                        HintOptions = new SeriesHintOptions
                        {

                            PointTextPattern = "{A$E hh:mm}: {V} m/s",
                        },
                        AxisY = new NumericAxisY
                        {
                            AutoRangeMode = AutoRangeMode.VisibleValues,
                            DisplayPosition = new AxisDisplayPositionNear(),
                        },
                        MarkersVisible = true
                    }.Assign(out SplineSeries splineSeries)
                }
            };


            dataAdapter.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.WindSpeed), Type = DevExpress.Maui.Charts.ValueType.Value });
        }
    }
}
