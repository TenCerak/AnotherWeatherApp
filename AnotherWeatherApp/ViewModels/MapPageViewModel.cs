﻿using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MapPageViewModel : BaseViewModel
{
    public MapPageViewModel(IAnalyticsService analyticsService, IDispatcher dispatcher) : base(analyticsService, dispatcher)
    {
    }

    [RelayCommand]
    public void TrackEvent()
    {
        AnalyticsService.Track("MainPageViewModel.TrackEvent");
    }
}

