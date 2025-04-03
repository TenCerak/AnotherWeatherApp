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
    public class PressureHourlyForecastChartView : ContentView
    {
        public PressureHourlyForecastChartView(in DetailForecastViewModel model)
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
                    
                    new AreaSeries
                    {
                        Data = new SeriesDataAdapter
                        {
                            ArgumentDataMember = "Time",
                            AllowLiveDataUpdates = false,
                        }.Assign(out SeriesDataAdapter dataAdapterPressure)
                        .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts),
                        Style = new AreaSeriesStyle
                        {
                            Stroke = Colors.Purple,
                            Fill = Colors.Purple,
                            FillEffect = new TransparencyGradient()
                            {
                                SeriesLineAlpha = 0.5f,
                            }
                        },
                        
                        DisplayName = "Pressure",
                        HintOptions = new SeriesHintOptions
                        {

                            PointTextPattern = "{A$E hh:mm}: {V} Hpa",
                        },
                        AxisY = new NumericAxisY
                        {
                            AutoRangeMode = AutoRangeMode.VisibleValues,
                            DisplayPosition = new AxisDisplayPositionNear(),
                            Layout = new AxisLayout()
                            {
                                Anchor1 = 0,
                                Anchor2 = 0.45,
                            },
                            AlwaysShowZeroLevel = false,
                            
                        },
                        MarkersVisible = true,
                        
                    },
                    new SplineSeries
                    {
                        Data = new SeriesDataAdapter
                        {
                            ArgumentDataMember = "Time",
                            AllowLiveDataUpdates = false,
                        }.Assign(out SeriesDataAdapter dataAdapterHumidity)
                        .Bind(SeriesDataAdapter.DataSourceProperty, static (DetailForecastViewModel vm) => vm.HourlyForecasts),
                        Style = new LineSeriesStyle
                        {
                            Stroke = Colors.Blue,
                        },

                        DisplayName = "Humidity",
                        HintOptions = new SeriesHintOptions
                        {
                            PointTextPattern = "{A$E hh:mm}: {V}",
                        },
                        AxisY = new NumericAxisY
                        {
                            AutoRangeMode = AutoRangeMode.VisibleValues,
                            DisplayPosition = new AxisDisplayPositionNear(),
                            Layout = new AxisLayout()
                            {
                                Anchor1 = 0.55,
                                Anchor2 = 1,
                            },
                            AlwaysShowZeroLevel = false
                        },
                        MarkersVisible = true,

                    }

                }
            };



            dataAdapterPressure.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.Pressure), Type = DevExpress.Maui.Charts.ValueType.Value });
            dataAdapterHumidity.ValueDataMembers.Add(new ValueDataMember { Member = nameof(HourlyForecast.Humidity), Type = DevExpress.Maui.Charts.ValueType.Value });
        }
    }
}
