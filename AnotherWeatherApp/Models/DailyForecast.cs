using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public class DailyForecast
    {
        public string LocationName { get; set; }
        public DateTime Time { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public string Description { get; set; }
        public ImageSource IconSource { get; set; }
        public float Temperature { get; set; }
        public float TemperatureMin { get; set; }
        public float TemperatureMax { get; set; }
        public float TemperatureNight { get; set; }
        public float TemperatureEvening { get; set; }
        public float TemperatureMorning { get; set; }
        public float FeelsLike { get; set; }
        public float FeelsLikeNight { get; set; }
        public float FeelsLikeEvening { get; set; }
        public float FeelsLikeMorning { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float? RainPrecipitation { get; set; }
        public float? SnowPrecipitation { get; set; }
        public float PrecipitationProbability { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public float WindGust { get; set; }
        public int Clouds { get; set; }

    }
}
