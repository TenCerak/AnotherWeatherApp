using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public partial class LongTermForecastViewModel : BaseViewModel
    {
        public LongTermForecastViewModel(IAnalyticsService analyticsService, IDispatcher dispatcher) : base(analyticsService, dispatcher)
        {
        }

        [RelayCommand]
        public void TrackEvent()
        {
            AnalyticsService.Track("LongTermForecastViewModel.TrackEvent");
        }
    }
}
