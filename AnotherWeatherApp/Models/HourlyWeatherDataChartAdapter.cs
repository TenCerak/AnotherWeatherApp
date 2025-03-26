using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public partial class HourlyWeatherDataChartAdapter : ObservableObject, IXYSeriesData
    {
        public static BindableProperty HourlyForecastProperty = BindableProperty
            .Create(
                nameof(HourlyForecast),
                typeof(HourlyForecastResponse),
                typeof(HourlyWeatherDataChartAdapter),
                null
            );

        [ObservableProperty]
        HourlyForecastResponse? hourlyForecast;

        int IXYSeriesData.GetDataCount()
        {
            return HourlyForecast?.list?.Count ?? 0;
        }

        SeriesDataType IXYSeriesData.GetDataType()
        {
            return SeriesDataType.Numeric;
        }

        DateTime IXYSeriesData.GetDateTimeArgument(int index)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds((long)(HourlyForecast?.list.ToArray()[index].dt ?? 0)).LocalDateTime;
        }

        object IXYSeriesData.GetKey(int index)
        {
            return HourlyForecast?.list?.ToArray()?[index] ?? null;
        }

        double IXYSeriesData.GetNumericArgument(int index)
        {
            return Convert.ToDouble((object?)(HourlyForecast?.list?.ToArray()?[index].main.temp));
        }

        string IXYSeriesData.GetQualitativeArgument(int index)
        {
            return HourlyForecast?.list?.ToArray()?[index].weather.FirstOrDefault()?.description ?? string.Empty;
        }

        double IXYSeriesData.GetValue(DevExpress.Maui.Charts.ValueType valueType, int index)
        {
            return Convert.ToDouble((object?)(HourlyForecast?.list?.ToArray()?[index].main.temp));
        }
    }
}
