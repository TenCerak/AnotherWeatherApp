﻿using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.Services;
using AnotherWeatherApp.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public partial class DetailForecastViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly OpenWeatherMapService _weatherService;
        [ObservableProperty]
        string? currentWeather;

        public DetailForecastViewModel(IAnalyticsService analyticsService, IDispatcher dispatcher, OpenWeatherMapService weatherService) : base(analyticsService, dispatcher)
        {
            _weatherService = weatherService;

            LoadDataAsync().ConfigureAwait(false);
        }

        public async Task LoadDataAsync()
        {
            Location location = new Location(0, 0);
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                AnalyticsService.Report(ex);
            }

            CurrentWeather = await _weatherService.GetCurrentWeatherAsync(location, CancellationToken.None);
        }


        [RelayCommand]
        public void TrackEvent()
        {
            AnalyticsService.Track("DetailForecastViewModel.TrackEvent");
        }
    }
}
