using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public class HourlyForecast
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public float Temperature { get; set; }
        public float FeelsLike { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float? RainPrecipitation { get; set; }
        public float? SnowPrecipitation { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public float WindGust { get; set; }
    }
}
