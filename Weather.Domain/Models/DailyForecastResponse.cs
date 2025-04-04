﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Models
{

    public class DailyForecastResponse
    {
        public City city { get; set; }
        public string cod { get; set; }
        public float message { get; set; }
        public int cnt { get; set; }
        public List<DailyForecast> list { get; set; }
    }

    public class DailyForecast
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public Temp temp { get; set; }
        public Feels_Like feels_like { get; set; }
        public float pressure { get; set; }
        public float humidity { get; set; }
        public Weather[] weather { get; set; }
        public float speed { get; set; }
        public int deg { get; set; }
        public float gust { get; set; }
        public int clouds { get; set; }
        public float pop { get; set; }
        public float rain { get; set; }
        public float snow { get; set; }
    }

    public class Temp
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

    public class Feels_Like
    {
        public float day { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

}
