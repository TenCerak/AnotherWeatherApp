using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Domain.Models;


public partial class OpenWeatherMapService : IGeocodingService
{
    private const string _baseGeocodingUrl = "https://api.openweathermap.org/geo/1.0/direct?";


    public async Task<List<Feature>?> GetLocationsBasedOnCityNameAsync(string cityName, CancellationToken token = default, string language = "en", int limit = 10)
    {
        var request = new RestRequest(_baseGeocodingUrl, Method.Get);

        request.AddParameter("q", cityName);
        request.AddParameter("appid", _apiKey);
        request.AddParameter("lang", language);
        request.AddParameter("limit", limit);
        try
        {

            var response = await _restClient.ExecuteAsync(request, token).ConfigureAwait(false);
            return JsonSerializer.Deserialize<List<Feature>>(response?.Content) ?? null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting geocoding data", ex);
        }
    }

}

