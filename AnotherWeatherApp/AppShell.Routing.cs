﻿using AnotherWeatherApp.Pages;
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

            if (pageType == typeof(MainPage))
                return $"/{nameof(MainPage)}";

            if (pageType == typeof(NavPage))
                return $"/{nameof(NavPage)}";

            if (pageType == typeof(DetailForecastPage))
                return $"/{nameof(DetailForecastPage)}";
            throw new InvalidOperationException($"Unknown page type {pageType}");

        }
    }
}
