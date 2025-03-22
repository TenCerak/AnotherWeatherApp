﻿using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Models;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.Views;
using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Pages
{
    public class DetailForecastPage : BaseContentPage<DetailForecastViewModel>
    {

        public DetailForecastPage(IAnalyticsService analyticsService, DetailForecastViewModel viewModel) : base(viewModel, analyticsService)
        {
            Content = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Detail Forecast" },
                    new Button {Text = "Get current weather", Command = new Command(async () => await BindingContext.LoadDataAsync().ConfigureAwait(true)) },
                    new Label { Text = "Current Weather" },

                    new Label().Bind(Label.TextProperty, getter: static (DetailForecastViewModel vm) => vm.Temperature, stringFormat: "Temperature: {0} °C"),
                }
            };
        }
    }


}
