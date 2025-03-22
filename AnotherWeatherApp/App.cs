using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.ViewPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp
{
    partial class App : Application
    {
        readonly AppShell _appShell;
        readonly IAnalyticsService _analyticsService;
        public App(AppShell appshell,IAnalyticsService analyticsService
            )
        {
            _appShell = appshell;
            _analyticsService = analyticsService;
        }

        protected override void OnStart()
        {
            _analyticsService.Track("App.OnStart");
        }

        protected override Window CreateWindow(IActivationState? activationState) => new(_appShell);
    }
}
