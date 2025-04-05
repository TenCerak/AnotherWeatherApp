using System;
using System.Linq;
using Weather.Domain.Models;


public interface IWeatherService
{
    Task<CurrentWeather?> GetCurrentWeatherAsync(double latitude, double longitude, CancellationToken token = default, string language = "en", string units = "metric");
    Task<HourlyForecastResponse?> GetHourlyForecastAsync(double latitude, double longitude, CancellationToken token = default, string language = "en", string units = "metric");
    Task<DailyForecastResponse?> GetDailyForecastAsync(double latitude, double longitude, CancellationToken token = default, int numberOfDays = 14 ,string language = "en", string units = "metric");
    string GetImageSourceForWeatherAsync(Weather.Domain.Models.Weather weather);
}

