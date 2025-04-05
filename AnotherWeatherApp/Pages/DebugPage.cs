using AnotherWeatherApp.Converters;
using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Models;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.Views;
using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Domain.Models;

namespace AnotherWeatherApp.Pages
{
    public class DebugPage : BaseContentPage<DetailForecastViewModel>
    {
        public DebugPage(IAnalyticsService analytics, DetailForecastViewModel viewModel ) : base(viewModel, analytics)
        {
            Title = "Navigation";
            StringConverter converter = new();
            Content = new ScrollView()
            {
                Content = new VerticalStackLayout()
                {
                    Children =
                    {
                        new Label() { Text = "Debug page"},
                        new Button()
                        {
                            Command = new Command(async () =>
                            {
                                await viewModel.LoadDataAsync().ConfigureAwait(true);
                            })
                        }
                        .Text("Reload data"),
                         new Label().Bind<Label, DetailForecastViewModel, HourlyForecastResponse, string>(Label.TextProperty,
                            getter: static (DetailForecastViewModel vm) => vm.Forecast, converter: converter)
                    }   
                }
            };
        }
    }
}
