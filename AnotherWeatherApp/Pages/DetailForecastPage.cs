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


                Content = new FlexLayout()
                {
                    JustifyContent = FlexJustify.Start,
                    AlignContent = FlexAlignContent.Stretch,
                    Direction = FlexDirection.Column,
                    GestureRecognizers =
                    {
                        new SwipeGestureRecognizer()
                        {
                            Direction = SwipeDirection.Down,
                            Command = new Command(async () => await  BindingContext.LoadDataAsync().ConfigureAwait(false))
                        }
                    },
                    Children = {
                        
                        new CurrentWeatherContentView(viewModel),
                        new TabView()
                        {
                            ItemsSource = new List<TabViewItem>()
                            {
                                new TabViewItem()
                                {
                                    HeaderText = "Forecast",
                                    Content = new BasicHourlyForecastChartView(viewModel),
                                },
                                 new TabViewItem()
                                {
                                    HeaderText = "Test",
                                    Content = new ScrollView()
                                    {
                                        Content = new StackLayout()
                                        {
                                        Children = {
                                            new Label().Text("Test").TextColor(Colors.White)
                                        }
                                            }
                                    }
                                }
                            },
                            
                        }.Shrink(700)
                    }

                }


            }
            .Bind(RefreshView.IsRefreshingProperty, static (DetailForecastViewModel vm) => vm.IsLoading);

        }

    }




    public class StringConverter : IValueConverter
    {
        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return JsonSerializer.Serialize(value, options: new() { WriteIndented = true });
        }

        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }


}
