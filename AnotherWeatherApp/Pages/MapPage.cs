using AnotherWeatherApp.Helpers;
using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using CommunityToolkit.Maui.Markup;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Tiling.Layers;
using Microsoft.Extensions.Configuration;

namespace AnotherWeatherApp.ViewPages;

public class MapPage : BaseContentPage<MapPageViewModel>
{
    Mapsui.UI.Maui.MapControl mapControl;
    MemoryLayer locationLayer = new("Location");
    public MapPage(MapPageViewModel model, IAnalyticsService analyticsService, IConfiguration configuration) : base(model, analyticsService)
    {
        Mapsui.UI.Maui.MapControl.UseGPU = true;
        mapControl = new Mapsui.UI.Maui.MapControl();
        mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer("AnotherWeatherMap"));
        mapControl.Map?.Layers.Add(locationLayer);
        

        var apiKey = configuration["OpenWeatherMap:ApiKey"];
        var weatherLayer = new TileLayer(new HttpTileSource(new GlobalSphericalMercator(),
            $"https://tile.openweathermap.org/map/precipitation_new/{{z}}/{{x}}/{{y}}.png?appid={apiKey}",
            name: "OWM-Clouds",
            tileFetcher: null))
        {
            Opacity = 1,
            Name = "Precipiation"
        };

        mapControl.Map?.Layers.Add(weatherLayer);
        
        Title = Properties.Resources.Map;
        Content = mapControl;
    }

    protected override void OnAppearing()
    {
#if ANDROID
        mapControl.IsVisible = false;
        mapControl.IsVisible = true;
#endif
        base.OnAppearing();
        AnalyticsService.Track($"{GetType().Name} appeared");
        Dispatcher.Dispatch(async () => await CenterOnMyLocation().ConfigureAwait(true));
        
    }

    private async Task CenterOnMyLocation()
    {
        var location = await  FavouriteStorage.GetCurrentLocation().ConfigureAwait(true);
        if(location is null) return;

        var point = SphericalMercator.FromLonLat(location.Longitude, location.Latitude).ToMPoint();

        Dispatcher.Dispatch(() => mapControl.Map.Navigator.CenterOnAndZoomTo(point, mapControl.Map.Navigator.Resolutions[9]));

        PointFeature pointFeature = new(point);

        List<IFeature> features = [pointFeature];

        locationLayer.Features = features;

    }
}