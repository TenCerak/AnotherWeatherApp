﻿using AnotherWeatherApp.Pages;
using AnotherWeatherApp.ViewPages;
using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp
{
    partial class AppShell : Shell
    {
        public AppShell(MapPage mapPage, DebugPage debugPage,DetailForecastPage detailForecastPage, LongTermForecastPage longTermForecastPage, LocationSettingsPage locationSetPage)
        {
            Items.Add(detailForecastPage);
            Items.Add(longTermForecastPage);
            Items.Add(mapPage);
            Items.Add(locationSetPage);

            FlyoutHeader = new Image()
                .Source(ImageSource.FromFile("logo.svg"))
                .Aspect(Aspect.AspectFit)
                .Size(100)
                .CenterHorizontal()
                .Margins(10);

            FlyoutBackgroundColor = Colors.Gray;
            this.AppThemeColorBinding(AppShell.FlyoutBackgroundColorProperty, Colors.WhiteSmoke, Colors.Black);
        }

    }
}
