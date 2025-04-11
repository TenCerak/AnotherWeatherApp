using AnotherWeatherApp.Helpers;
using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Models;
using AnotherWeatherApp.Pages.Base;
using AnotherWeatherApp.ViewModels;
using CommunityToolkit.Maui.Markup;
using DevExpress.Maui.CollectionView;
using DevExpress.Maui.Core;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Models;

namespace AnotherWeatherApp.Pages
{
    public class LocationSettingsPage : BaseContentPage<LocationSettingsViewModel>
    {
        SearchBar searchBar;
        public LocationSettingsPage(IAnalyticsService analyticsService, LocationSettingsViewModel model) : base(in model,in analyticsService)
        {
            Title = "Location Settings";

            Content = new StackLayout()
            {
                new HorizontalStackLayout()
                {
                    new Label().Text("Use device location"),
                    new Switch()
                        .Bind(Switch.IsToggledProperty, static (LocationSettingsViewModel vm) => vm.UseCurrentLocation),
                },
                new SearchBar()
                .Placeholder("Search")
                .Assign(out SearchBar search),


                new DXCollectionView()
                {
                    ItemTemplate = new DataTemplate(() =>
                    {
                        return new DXBorder()
                        {
                            Margin = new Thickness(10),
                            CornerRadius = 8,
                            BorderColor = Colors.DarkGray,
                            BorderThickness = 2,
                            Content = new HorizontalStackLayout()
                            {
                                Children =
                                {
                                    new Label()
                                    .Padding(10)
                                    .Bind(Label.TextProperty, nameof(Feature.name)),

                                    new Label()
                                    .Padding(10)    
                                    .Bind(Label.TextProperty, nameof(Feature.country)),

                                    new Label()
                                    .Padding(10)
                                    .Bind(Label.TextProperty, nameof(Feature.state)),

                                },
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        };
                    }),
                    SelectionMode = SelectionMode.Single,
                }
                .Bind(DXCollectionView.ItemsSourceProperty, nameof(LocationSettingsViewModel.SearchResults))
                .Bind(DXCollectionView.IsVisibleProperty, static (LocationSettingsViewModel vm) => vm.SearchVisible)
                .Assign(out DXCollectionView searchResult)
                ,

                new DXCollectionView()
                {
                    IsPullToRefreshEnabled = true,
                    PullToRefreshCommand = new Command(() =>
                    {
                        model.ReloadData();
                    }),
                    ItemTemplate = new DataTemplate(() =>
                    {
                        return new DXBorder()
                        {
                            Margin = new Thickness(10),
                            CornerRadius = 8,
                            BorderColor = Colors.DarkGray,
                            BorderThickness = 2,
                            Content = new HorizontalStackLayout()
                            {
                                Children =
                                {
                                    new Label()
                                    .Bind(Label.TextProperty, nameof(FavouriteLocation.Name)),
                                    new VerticalStackLayout()
                                    {
                                        new Label().Text("Use as current location").TextCenter().Center(),
                                        new Switch()
                                        .Bind(Switch.IsToggledProperty, nameof(FavouriteLocation.IsCurrentLocation))
                                        ,
                                    },

                                },
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            },
                            
                        };
                    }),

                }.Bind(DXCollectionView.ItemsSourceProperty,static  (LocationSettingsViewModel vm) => vm.FavouriteLocations),
            };
            searchBar = search;
            search.SearchButtonPressed += Search_SearchButtonPressed;
            search.TextChanged += Search_TextChanged;
            searchResult.SelectionChanged += SearchResult_SelectionChanged;

        }

        private void SearchResult_SelectionChanged(object? sender, CollectionViewSelectionChangedEventArgs e)
        {
            searchBar.UpdateText("");
            var item = e.AddedItems
                .OfType<Feature>()
                .FirstOrDefault();

            if (item is null) return;
            Dispatcher.Dispatch(() => ViewModel.AddFeatureAsFavourite(item));
            
        }

        private void Search_SearchButtonPressed(object? sender, EventArgs e)
        {
            ViewModel.SearchLocations((sender as SearchBar).Text).ConfigureAwait(false);
        }
        void Search_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ViewModel.SearchResults = new();
                ViewModel.SearchVisible = false;
            }
            else
            {
                ViewModel.SearchVisible = true;
            }
        }
    }
}
