using AnotherWeatherApp.Converters;
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
            var borderColorConverter = new LocationToBorderColorConverter();
            

            Title = Properties.Resources.LocationSettings;

            Content = new StackLayout()
            {
                #region Search
                new SearchBar()
                .Placeholder(Properties.Resources.SearchPlaceholder)
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
#endregion

                new DXCollectionView()
                {
                    PullToRefreshCommand = new Command(() =>
                    {
                        model.ReloadData();
                    }),
                    ItemTemplate = new DataTemplate(() =>
                    {
                        SwipeContainer container = new();

                        container.ItemView = new DXBorder()
                        {

                            Margin = new Thickness(10),
                            CornerRadius = 8,
                            BorderThickness = 2,
                            Content = new HorizontalStackLayout()
                            {
                                Children =
                                {
                                    new Label()
                                    .Bind(Label.TextProperty, nameof(FavouriteLocation.Name))
                                    .Padding(10),
                                },
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }                           
                            
                        }
                        .Bind(DXBorder.BorderColorProperty, nameof(FavouriteLocation.IsCurrentLocation),converter: borderColorConverter);

                        container.FullSwipeMode = FullSwipeMode.End;

                        container.EndSwipeItems.Add(new SwipeContainerItem()
                        {
                            Caption = Properties.Resources.Delete,
                            BackgroundColor = Colors.Red,
                            Image = ViewModel.DeleteIcon,
                            Command = new Command(() =>
                            {
                                var item = container.BindingContext as FavouriteLocation;
                                ViewModel.RemoveLocation(item);
                            }),

                        }
                        .Bind(IsEnabledProperty,nameof(FavouriteLocation.CanBeDeleted))
                        .Bind(SwipeContainerItem.ImageProperty, static (LocationSettingsViewModel vm) => vm.DeleteIcon)
                        );
                        

                        return container;
                    }),
                    SelectionMode = SelectionMode.Single,
                    AllowDragDropItems = true,
                    AllowDragDropSortedItems = true,


                }
                .Bind(DXCollectionView.ItemsSourceProperty,static  (LocationSettingsViewModel vm) => vm.FavouriteLocations)
                .Assign(out DXCollectionView favLocations)
            };
            searchBar = search;
            search.SearchButtonPressed += Search_SearchButtonPressed;
            search.TextChanged += Search_TextChanged;
            searchResult.SelectionChanged += SearchResult_SelectionChanged;
            favLocations.SelectionChanged += FavLocations_SelectionChanged;
            favLocations.CompleteItemDragDrop += FavLocations_CompleteItemDragDrop;
        }

        private void FavLocations_CompleteItemDragDrop(object? sender, CompleteItemDragDropEventArgs e)
        {
            foreach (var item in (ViewModel.FavouriteLocations))
            {
                int order = (sender as DXCollectionView).FindItemHandle(item);
                ViewModel.SetOrderToFavLocation(item, order);
            }
        }

        void FavLocations_SelectionChanged(object? sender, CollectionViewSelectionChangedEventArgs e)
        {
            var item = e.AddedItems
                .OfType<FavouriteLocation>()
                .FirstOrDefault();

            if (item is null) return;
            Dispatcher.Dispatch(() => ViewModel.SetLocationAsCurrent(item));

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
