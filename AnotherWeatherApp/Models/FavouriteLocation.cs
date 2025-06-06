﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Models
{
    public class FavouriteLocation
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public bool IsFavourite { get; set; }
        public bool CanBeDeleted { get; set; } = true;
        public bool IsCurrentLocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public FavouriteLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public FavouriteLocation()
        {

        }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }
}
