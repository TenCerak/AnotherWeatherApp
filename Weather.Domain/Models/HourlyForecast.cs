using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HourlyForecast
{
    public string cod { get; set; }
    public int message { get; set; }
    public int cnt { get; set; }
    public List<Forecast> list { get; set; }
    public City city { get; set; }
}

public class City
{
    public int id { get; set; }
    public string name { get; set; }
    public Coord coord { get; set; }
    public string country { get; set; }
    public int population { get; set; }
    public int timezone { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class Forecast
{
    public int dt { get; set; }
    public Main main { get; set; }
    public List<Weather> weather { get; set; }
    public Clouds clouds { get; set; }
    public Wind wind { get; set; }
    public int visibility { get; set; }
    public float pop { get; set; }
    public Precipitation rain { get; set; }
    public Precipitation snow { get; set; }
    public Sys sys { get; set; }
    public string dt_txt { get; set; }
}



