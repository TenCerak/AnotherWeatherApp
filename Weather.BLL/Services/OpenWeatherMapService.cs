using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


public class OpenWeatherMapService : IDisposable, IWeatherService
{
    private readonly IConfiguration _configuration;
    private RestClient _restClient;
    private const string _baseUrl = "https://api.openweathermap.org/data/2.5/";
    private readonly string _apiKey;
    public OpenWeatherMapService(IConfiguration configuration)
    {
        _configuration = configuration;

        _apiKey = _configuration["OpenWeatherMap:ApiKey"] ?? string.Empty;

        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new ArgumentNullException("OpenWeatherMap:ApiKey");
        }

        _restClient = new RestClient(_baseUrl);
    }



    public async Task<CurrentWeather?> GetCurrentWeatherAsync(double latitude, double longitude, CancellationToken token = default)
    {
        //https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}

        var request = new RestRequest("weather", Method.Get);
        request.AddParameter("units", "metric");
        request.AddParameter("lat", latitude);
        request.AddParameter("lon", longitude);
        request.AddParameter("appid", _apiKey);

        try
        {
            var response = await _restClient.ExecuteAsync(request, token).ConfigureAwait(false);

            return JsonSerializer.Deserialize<CurrentWeather>(response?.Content) ?? null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting weather data", ex);
        }
    }

    public void Dispose()
    {
        _restClient?.Dispose();
    }
}

