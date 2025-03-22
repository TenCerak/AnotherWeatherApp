using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Pages.Base
{
    public abstract class BaseContentPage : ContentPage
    {
        protected IAnalyticsService AnalyticsService { get; }
        protected BaseContentPage(in IAnalyticsService analyticsService)
        {
            AnalyticsService = analyticsService;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AnalyticsService.Track($"{GetType().Name} appeared");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AnalyticsService.Track($"{GetType().Name} disappeared");
        }
    }


    public  abstract class BaseContentPage<T> : BaseContentPage where T : BaseViewModel
    {
        protected T ViewModel { get; }
        protected BaseContentPage(in T viewModel, in IAnalyticsService analyticsService)
            : base(analyticsService)
        {
            base.BindingContext = viewModel;
        }
    }
}
