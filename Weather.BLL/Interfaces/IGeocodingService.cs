using System;
using System.Linq;
using Weather.Domain.Models;


public interface IGeocodingService
{
    Task<List<Feature>?> GetLocationsBasedOnCityNameAsync(string cityName, CancellationToken token = default, string language = "en", int limit = 10);
}

