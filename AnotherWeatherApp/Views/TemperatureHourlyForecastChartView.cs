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
    public class TemperatureHourlyForecastChartView : ContentView
    {
        public TemperatureHourlyForecastChartView(in DetailForecastViewModel model)
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
                        }.Assign(out SeriesDataAdapter dataAdapterNormal)
                        .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts),
                        Style = new LineSeriesStyle
                        {
                            Stroke = Colors.Orange,
                            MarkerStyle = new MarkerStyle
                            {
                                Fill = Colors.Orange,
                                Stroke = Colors.White,
                            },

                        },
                        DisplayName = "Temperature",
                        HintOptions = new SeriesHintOptions
                        {

                            PointTextPattern = "{A$E hh:mm}: {V}°C",
                        },
                        AxisY = new NumericAxisY
                        {
                            AutoRangeMode = AutoRangeMode.AllValues,
                            DisplayPosition = new AxisDisplayPositionNear(),
                        },
                        MarkersVisible= true
                    }.Assign(out SplineSeries splineSeries)
                    ,
                    new BarSeries
                    {
                        DisplayName = "Rain",
                        Data = new SeriesDataAdapter
                        {
                            ArgumentDataMember = "Time",
                            AllowLiveDataUpdates = true,
                        }.Assign(out SeriesDataAdapter dataAdapterRain)
                        .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts),
                        Style = new BarSeriesStyle
                        {
                            Fill = Colors.Blue,
                        },
                        HintOptions = new SeriesHintOptions
                        {
                            PointTextPattern = "{A$E hh:mm}: {V} mm/h",
                        },
                        AxisY = new NumericAxisY()
                        {
                            DisplayPosition = new AxisDisplayPositionFar(),
                            Label = new AxisLabel
                            {
                                Visible = false,
                            },
                            Layout = new AxisLayout
                            {
                                Anchor1 = 0,
                                Anchor2 = 0.25,
                            },
                        }
                    },
                    new BarSeries
                    {
                        Data = new SeriesDataAdapter
                        {
                            ArgumentDataMember = "Time",
                            AllowLiveDataUpdates = true,

                        }.Assign(out SeriesDataAdapter dataAdapterSnow)
                        .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts),
                        Style = new BarSeriesStyle
                        {
                            Fill = Colors.White,
                        },
                        DisplayName = "Snow",
                        HintOptions = new SeriesHintOptions
                        {
                            PointTextPattern = "{A$E hh:mm}: {V} mm/h",
                        },
                        AxisY = new NumericAxisY()
                        {
                            DisplayPosition = new AxisDisplayPositionFar(),
                            Label = new AxisLabel
                            {
                                Visible = false,
                            },
                            Layout = new AxisLayout
                            {
                                Anchor1 = 0,
                                Anchor2 = 0.25,
                            },
                        }
                    }
                }
            };



            dataAdapterNormal.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.Temperature), Type = DevExpress.Maui.Charts.ValueType.Value });

            dataAdapterRain.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.RainPrecipitation), Type = DevExpress.Maui.Charts.ValueType.Value });
            dataAdapterSnow.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.SnowPrecipitation), Type = DevExpress.Maui.Charts.ValueType.Value });

            GradientPointBasedSegmentColorizer colorizer = new GradientPointBasedSegmentColorizer();
            ValueBandPointColorizer valueBandPointColorizer = new ValueBandPointColorizer();
            valueBandPointColorizer.ColorStops.Add(new ColorStop { Value1 = -100, Value2 = -10, Color = Colors.Blue });
            valueBandPointColorizer.ColorStops.Add(new ColorStop { Value1 = -10, Value2 = 0, Color = Colors.LightBlue });
            valueBandPointColorizer.ColorStops.Add(new ColorStop { Value1 = 0, Value2 = 10, Color = Colors.Yellow });
            valueBandPointColorizer.ColorStops.Add(new ColorStop { Value1 = 10, Value2 = 25, Color = Colors.Orange });
            valueBandPointColorizer.ColorStops.Add(new ColorStop { Value1 = 25, Value2 = 100, Color = Colors.Red });
            colorizer.PointColorizer = valueBandPointColorizer;

            splineSeries.SegmentColorizer = colorizer;

        }
    }
}
