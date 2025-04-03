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
using DevExpress.Maui.Charts;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using Microsoft.Maui.Layouts;
using DevExpress.Maui.Controls;
using AnotherWeatherApp.Converters;

namespace AnotherWeatherApp.Pages
{
    public class DetailForecastPage : BaseContentPage<DetailForecastViewModel>
    {
        StringConverter StringConverter = new StringConverter();
        public DetailForecastPage(IAnalyticsService analyticsService, DetailForecastViewModel viewModel) : base(viewModel, analyticsService)
        {
            
            this.Title = Properties.Resources.DetailForecastTitle;
            Content = new RefreshView()
            {
                Command = new Command(async () => await BindingContext.LoadDataAsync().ConfigureAwait(true)),


                Content = new Grid()
                {
                    ColumnDefinitions = Columns.Define(Star),
                    RowDefinitions = Rows.Define(Auto,Star),
                    GestureRecognizers =
                    {
                        new SwipeGestureRecognizer()
                        {
                            Direction = SwipeDirection.Down,
                            Command = new Command(async () => await  BindingContext.LoadDataAsync().ConfigureAwait(false))
                        }
                    },
                    Children = {

                        new CurrentWeatherContentView(viewModel).Row(0).Column(0),
                        new TabView().Row(1).Column(0)
                        .Assign(out TabView tabView)
                    }

                }


            }
            .Bind(RefreshView.IsRefreshingProperty, static (DetailForecastViewModel vm) => vm.IsLoading);

            

            tabView.Items.Add(new TabViewItem()
            {
                HeaderText = Properties.Resources.Temperature,
                Content = new TemperatureHourlyForecastChartView(viewModel)                     
            });

            tabView.Items.Add(new TabViewItem()
            {
                HeaderText = Properties.Resources.Pressure,
                Content = new PressureHourlyForecastChartView(viewModel)
            });

            tabView.Items.Add(new TabViewItem()
            {
                HeaderText = Properties.Resources.Wind,
                Content = new WindHourlyForecastChartView(viewModel)
            });

        }

    }
}
