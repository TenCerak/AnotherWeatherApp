using AnotherWeatherApp.Helpers;
using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using CommunityToolkit.Maui.Markup;
using Mapsui.Projections;
using Mapsui.Tiling.Layers;
using Microsoft.Extensions.Configuration;

namespace AnotherWeatherApp.ViewPages;

public class MapPage : BaseContentPage<MapPageViewModel>
{
    Mapsui.UI.Maui.MapControl mapControl;
    public MapPage(MapPageViewModel model, IAnalyticsService analyticsService, IConfiguration configuration) : base(model, analyticsService)
    {
        
        mapControl = new Mapsui.UI.Maui.MapControl();
        mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());


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
        base.OnAppearing();
        AnalyticsService.Track($"{GetType().Name} appeared");
    }
}