using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.Services;
using AnotherWeatherApp.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public partial class DetailForecastViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IWeatherService _weatherService;
        [ObservableProperty]
        CurrentWeather? currentWeather;

        [ObservableProperty]
        HourlyForecastResponse? forecast;

        [ObservableProperty]
        ImageSource iconSource;

        [ObservableProperty]
        string? description;

        [ObservableProperty]
        ObservableCollection<HourlyForecast> hourlyForecasts;



        public DetailForecastViewModel(IAnalyticsService analyticsService, IDispatcher dispatcher, IWeatherService weatherService) : base(analyticsService, dispatcher)
        {
            _weatherService = weatherService;
            
            LoadDataAsync().ConfigureAwait(true);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CurrentWeather) && CurrentWeather is not null)
            {
                IconSource = new UriImageSource() 
                {
                    Uri = new Uri(_weatherService.GetImageSourceForWeatherAsync(CurrentWeather.weather[0])),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1,0,0) 
                };
                Description = CurrentWeather.weather[0].description;
            }
            base.OnPropertyChanged(e);
        }

        public async Task LoadDataAsync()
        {
            Location location = new Location(0, 0);
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync().ConfigureAwait(false);

                var CurrentWeatherTask = _weatherService.GetCurrentWeatherAsync(
                        location.Latitude,
                        location.Longitude,
                        CancellationToken.None,
                        CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                    );

                var ForecastTask = _weatherService.GetHourlyForecastAsync(
                        location.Latitude,
                        location.Longitude,
                        CancellationToken.None,
                        CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                    );

                await Task.WhenAll(new List<Task>{ ForecastTask, CurrentWeatherTask }).ConfigureAwait(false);

                CurrentWeather = CurrentWeatherTask.Result;
                Forecast = ForecastTask.Result;

                if (Forecast is null) return;
                if (HourlyForecasts is null) HourlyForecasts = new();

                HourlyForecasts.Clear();
                foreach (var item in Forecast.list)
                {

                    HourlyForecasts.Add(new()
                    {
                        Time = DateTimeOffset.FromUnixTimeSeconds(item.dt).LocalDateTime,
                        Temperature = item.main.temp,
                        Description = item.weather[0].description,
                        FeelsLike = item.main.feels_like,
                        Humidity = item.main.humidity,
                        Pressure = item.main.pressure,
                        WindSpeed = item.wind.speed,
                        WindDirection = item.wind.deg,
                        WindGust = item.wind.gust,
                        RainPrecipitation = item.rain?._1h ?? 0,
                        SnowPrecipitation = item.snow?._1h ?? 0


                    });
                }


            }
            catch (Exception ex)
            {
                AnalyticsService.Report(ex);
            }

        }


        [RelayCommand]
        public void TrackEvent()
        {
            AnalyticsService.Track("DetailForecastViewModel.TrackEvent");
        }
    }
}
