using AnotherWeatherApp.Helpers;
using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Models;
using AnotherWeatherApp.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Models;

namespace AnotherWeatherApp.ViewModels
{
    public partial class LocationSettingsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [ObservableProperty]
        bool? isLoading = false;

        [ObservableProperty]
        List<Weather.Domain.Models.Feature> searchResults;

        [ObservableProperty]
        Feature selectedResult;

        [ObservableProperty]
        bool searchVisible;

        [ObservableProperty]
        ImageSource deleteIcon;

        public bool UseCurrentLocation
        {
            get => FavouriteStorage.UseCurrentLocation;
            set
            {
                FavouriteStorage.UseCurrentLocation = value;
                OnPropertyChanged(nameof(LocationSettingsViewModel.UseCurrentLocation));
            }
        }

        private readonly IGeocodingService _geocodingService;

        [ObservableProperty]
        List<Models.FavouriteLocation> favouriteLocations = new();

        public LocationSettingsViewModel(IGeocodingService geocodingService, IAnalyticsService analyticsService, IDispatcher dispatcher) : base(analyticsService, dispatcher)
        {
            _geocodingService = geocodingService;
            FavouriteLocations = FavouriteStorage.LoadFavorites();
            deleteIcon = ImageSource.FromFile(@"delete.png");
            ReloadData();
        }

        public void ReloadData()
        {
            IsLoading = true;
            var locations = FavouriteStorage.LoadFavorites();

            if (locations.Any(x => x.Name == Properties.Resources.CurrentLocation) == false)
            {
                FavouriteLocation cur = new()
                {
                    Name = Properties.Resources.CurrentLocation,
                    IsCurrentLocation = UseCurrentLocation,
                    Order = -1,
                };
                locations.Add(cur);
            }
            locations = locations.OrderBy(x => x.Order).ToList();
            FavouriteLocations = locations;
            IsLoading = false;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(this.SelectedResult) == false) return;
            if (SelectedResult is null) return;
            AddFeatureAsFavourite(SelectedResult);
            SearchResults = new();

        }

        public async Task SearchLocations(string query)
        {
            var result = await _geocodingService.GetLocationsBasedOnCityNameAsync(query)
                .ConfigureAwait(true) ?? null;

            if (result is null) return;
            SearchResults = result;
        }

        public void SetLocationAsCurrent(FavouriteLocation location)
        {
            var favourites = FavouriteStorage.LoadFavorites();

            if (location.Name == Properties.Resources.CurrentLocation) UseCurrentLocation = true;
            else UseCurrentLocation = false;

            foreach (var item in favourites)
            {
                item.IsCurrentLocation = false;
            }

            var existingLocation = favourites.FirstOrDefault(x => x.Latitude == location.Latitude && x.Longitude == location.Longitude);
            if (existingLocation != null)
            {
                existingLocation.IsCurrentLocation = true;
            }
            else
            {
                location.IsCurrentLocation = true;
                favourites.Add(location);
            }
            FavouriteStorage.SaveFavorites(favourites);
            Dispatcher.Dispatch(() => ReloadData());
        }

        public void SetOrderToFavLocation(FavouriteLocation location, int order)
        {
            var favourites = FavouriteStorage.LoadFavorites();
            var existingLocation = favourites.FirstOrDefault(x => x.Latitude == location.Latitude && x.Longitude == location.Longitude);
            if (existingLocation != null)
            {
                existingLocation.Order = order;
            }
            else
            {
                location.Order = order;
                favourites.Add(location);
            }
            FavouriteStorage.SaveFavorites(favourites);
        }

        public void AddFeatureAsFavourite(Feature feature)
        {
            FavouriteLocation location = new(feature.lat, feature.lon);

            location.Name = feature.name;
            location.Country = feature.country;
            location.State = feature.state;


            var favourites = FavouriteStorage.LoadFavorites();
            foreach (var item in favourites)
            {
                item.IsCurrentLocation = false;
            }
            var existingLocation = favourites.FirstOrDefault(x => x.Latitude == location.Latitude && x.Longitude == location.Longitude);
            if (existingLocation != null)
            {
                existingLocation.IsCurrentLocation = true;
            }
            else
            {
                location.IsCurrentLocation = true;
                favourites.Add(location);

                if (favourites.Any())
                    location.Order = favourites.Max(x => x.Order) + 1;
            }
            FavouriteStorage.SaveFavorites(favourites);
            Dispatcher.Dispatch(() => ReloadData());
        }
        public void RemoveLocation(FavouriteLocation? item)
        {
            if (item is null) return;
            if(item.CanBeDeleted == false) return;
            var favourites = FavouriteStorage.LoadFavorites();
            var existingLocation = favourites.FirstOrDefault(x => x.Latitude == item.Latitude && x.Longitude == item.Longitude);
            if (existingLocation is null) return;

            favourites.Remove(existingLocation);
            FavouriteStorage.SaveFavorites(favourites);
            Dispatcher.Dispatch(() => ReloadData());
        }
    }
}
