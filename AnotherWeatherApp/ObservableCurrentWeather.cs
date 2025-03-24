using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp
{
    public partial class ObservableCurrentWeather : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Temperature))]
        [NotifyPropertyChangedFor(nameof(TemperatureMin))]
        [NotifyPropertyChangedFor(nameof(TemperatureMax))]
        private CurrentWeather _currentWeather;

        public ObservableCurrentWeather(CurrentWeather currentWeather)
        {
            _currentWeather = currentWeather;
        }
        public string Temperature => CurrentWeather.main.temp.ToString();

        public string TemperatureMin => CurrentWeather.main.temp_min.ToString();

        public string TemperatureMax => CurrentWeather.main.temp_max.ToString();
    }
}
