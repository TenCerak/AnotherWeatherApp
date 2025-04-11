using AnotherWeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Helpers
{
    public static class FavouriteStorage
    {
        private const string FavoritesKey = "FavoriteLocations";

        public static bool UseCurrentLocation
        {
            get
            {
                return Preferences.Get("UseCurrentLocation", true);
            }
            set
            {
                Preferences.Set("UseCurrentLocation", value);
            }
        }

        public  static async Task<Location> GetCurrentLocation()
        {
            if (UseCurrentLocation)
            {
                 return await Geolocation.GetLastKnownLocationAsync().ConfigureAwait(true) ?? new Location();
            }
            var location = LoadFavorites().FirstOrDefault(x => x.IsCurrentLocation);
            if (location != null)
            {
                return new Location(location.Latitude,location.Longitude);
            }
            else
            {
                return new Location(0, 0);
            }
        }

        public static void SetCurrentLocation(FavouriteLocation location)
        {
            var favorites = LoadFavorites();
            var currentLocations = favorites.Where(x => x.IsCurrentLocation);
            foreach (var cur in currentLocations)
            {
                cur.IsCurrentLocation = false;
            }
            var newLocation = favorites.FirstOrDefault(x => x.Latitude == location.Latitude && x.Longitude == location.Longitude);
            if (newLocation != null)
            {
                newLocation.IsCurrentLocation = true;
            }
            else
            {
                favorites.Add(location);
            }
            SaveFavorites(favorites);
        }

        public static void SaveFavorites(List<Models.FavouriteLocation> favorites)
        {
            var json = JsonSerializer.Serialize(favorites);
            Preferences.Set(FavoritesKey, json);
        }

        public static List<Models.FavouriteLocation> LoadFavorites()
        {
            var json = Preferences.Get(FavoritesKey, string.Empty);
            return string.IsNullOrEmpty(json)
                ? new List<Models.FavouriteLocation>()
                : JsonSerializer.Deserialize<List<FavouriteLocation>>(json) ?? new List<FavouriteLocation>();
        }
    }
}
