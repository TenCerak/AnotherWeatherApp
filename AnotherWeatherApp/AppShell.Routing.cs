using AnotherWeatherApp.Pages;
using AnotherWeatherApp.ViewPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp
{
    partial class AppShell
    {
        public static string GetPageRoute<TPage>() where TPage : ContentPage
        {
            var pageType = typeof(TPage);

            if (pageType == typeof(MapPage))
                return $"/{nameof(MapPage)}";

            if (pageType == typeof(DebugPage))
                return $"/{nameof(DebugPage)}";

            if (pageType == typeof(DetailForecastPage))
                return $"/{nameof(DetailForecastPage)}";

            if (pageType == typeof(LongTermForecastPage))
                return $"/{nameof(LongTermForecastPage)}";

            if(pageType == typeof(LocationSettingsPage))
                return $"/{nameof(LocationSettingsPage)}";

            throw new InvalidOperationException($"Unknown page type {pageType}");

        }
    }
}
