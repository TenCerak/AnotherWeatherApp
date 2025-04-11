using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Models;

namespace AnotherWeatherApp.Models
{
    public partial class LongTermForecastViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [ObservableProperty]
        CurrentWeather? currentWeather;
        [ObservableProperty]
        DailyForecastResponse? forecast;

        [ObservableProperty]
        bool? isLoading = false;

        [ObservableProperty]
        ObservableCollection<DailyForecast> dailyForecasts = new();

        [ObservableProperty]
        ImageSource sunriseSourceLight;
        [ObservableProperty]
        ImageSource sunriseSourceDark;

        [ObservableProperty]
        ImageSource sunsetSourceLight;
        [ObservableProperty]
        ImageSource sunsetSourceDark;

        [ObservableProperty]
        ImageSource daySourceLight;
        [ObservableProperty]
        ImageSource daySourceDark;

        [ObservableProperty]
        ImageSource nightSourceLight;
        [ObservableProperty]
        ImageSource nightSourceDark;

        [ObservableProperty]
        ImageSource humiditySourceLight;
        [ObservableProperty]
        ImageSource humiditySourceDark;

        [ObservableProperty]
        ImageSource windSourceLight;
        [ObservableProperty]
        ImageSource windSourceDark;

        [ObservableProperty]
        ImageSource rainSourceLight;
        [ObservableProperty]
        ImageSource rainSourceDark;

        [ObservableProperty]
        ImageSource snowSourceLight;
        [ObservableProperty]
        ImageSource snowSourceDark;

        IWeatherService _weatherService;

        public LongTermForecastViewModel(IAnalyticsService analyticsService, IDispatcher dispatcher, IWeatherService weatherService) : base(analyticsService, dispatcher)
        {
            _weatherService = weatherService;
            SunriseSourceDark = ImageSource.FromFile(@"sunrise_white.png");
            SunriseSourceLight = ImageSource.FromFile(@"sunrise_dark.png");

            SunsetSourceDark = ImageSource.FromFile(@"sunset_white.png");
            SunsetSourceLight = ImageSource.FromFile(@"sunset_dark.png");
                
            DaySourceDark = ImageSource.FromFile(@"day_white.png");
            DaySourceLight = ImageSource.FromFile(@"day_dark.png");

            NightSourceDark = ImageSource.FromFile(@"night_white.png");
            NightSourceLight = ImageSource.FromFile(@"night_dark.png");

            HumiditySourceDark = ImageSource.FromFile(@"humidity_white.png");
            HumiditySourceLight = ImageSource.FromFile(@"humidity_dark.png");

            WindSourceDark = ImageSource.FromFile(@"wind_white.png");
            WindSourceLight = ImageSource.FromFile(@"wind_dark.png");

            RainSourceDark = ImageSource.FromFile(@"rain_white.png");
            RainSourceLight = ImageSource.FromFile(@"rain_dark.png");

            SnowSourceDark = ImageSource.FromFile(@"snow_white.png");
            SnowSourceLight = ImageSource.FromFile(@"snow_dark.png");


        }



        public async Task LoadDataAsync()
        {
            IsLoading = true;
            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location is null) return;

                CurrentWeather = await _weatherService.GetCurrentWeatherAsync(
                    location.Latitude,
                    location.Longitude
                    ).ConfigureAwait(true);

                Forecast = await _weatherService.GetDailyForecastAsync(
                    location.Latitude,
                    location.Longitude,
                    language: CultureInfo.CurrentCulture.Name.Split('-')[1]
                    ).ConfigureAwait(true);

                if (Forecast is null) return;

                DailyForecasts.Clear();

                foreach (var forecast in Forecast.list)
                {
                    DailyForecasts.Add(new DailyForecast()
                    {
                        LocationName = Forecast.city.name,
                        Time = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime,
                        Sunrise = DateTimeOffset.FromUnixTimeSeconds(forecast.sunrise).DateTime,
                        Sunset = DateTimeOffset.FromUnixTimeSeconds(forecast.sunset).DateTime,
                        Description = forecast.weather[0].description,


                        IconSource = new UriImageSource()
                        {
                            Uri = new Uri(_weatherService.GetImageSourceForWeatherAsync(forecast.weather[0])),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(1, 0, 0)
                        },
                        Temperature = forecast.temp.day,
                        TemperatureMin = forecast.temp.min,
                        TemperatureMax = forecast.temp.max,
                        TemperatureNight = forecast.temp.night,
                        TemperatureEvening = forecast.temp.eve,
                        TemperatureMorning = forecast.temp.morn,
                        FeelsLike = forecast.feels_like.day,
                        FeelsLikeNight = forecast.feels_like.night,
                        FeelsLikeEvening = forecast.feels_like.eve,
                        FeelsLikeMorning = forecast.feels_like.morn,
                        Pressure = forecast.pressure,
                        Humidity = forecast.humidity,
                        RainPrecipitation = forecast.rain,
                        SnowPrecipitation = forecast.snow,
                        PrecipitationProbability = forecast.pop,
                        WindSpeed = forecast.speed,
                        WindDirection = forecast.deg,
                        WindGust = forecast.gust,
                        Clouds = forecast.clouds
                    });
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
                AnalyticsService.Report(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }


    }
}
