using System;
using System.Linq;


public interface IWeatherService
{
    Task<CurrentWeather?> GetCurrentWeatherAsync(double latitude, double longitude, CancellationToken token = default, string language = "en", string units = "metric");

    string GetImageSourceForWeatherAsync(Weather weather);
}

