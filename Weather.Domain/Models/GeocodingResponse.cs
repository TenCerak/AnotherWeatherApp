using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Models
{
    public class GeocodingResponse
    {
        public Feature[] locations { get; set; }        
    }

    public class Feature
    {
        public string name { get; set; }
        public Dictionary<string, string> local_names { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }
}
