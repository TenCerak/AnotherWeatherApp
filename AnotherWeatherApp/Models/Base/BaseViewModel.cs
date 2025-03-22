using AnotherWeatherApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.ViewModels.Base
{
    public abstract class BaseViewModel(IAnalyticsService analyticsService,IDispatcher dispatcher) : ObservableObject
    {
        protected IDispatcher Dispatcher { get; } = dispatcher;
        protected IAnalyticsService AnalyticsService { get; } = analyticsService;
    }
}
