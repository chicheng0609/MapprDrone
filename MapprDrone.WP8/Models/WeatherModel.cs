using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapprDrone.WP8.Models
{
    public class WeatherModel
    {
        public string LocationName { get; set; }
        public string Temperature { get; set; }
        public string WindSpeed { get; set; }
        public string WeatherDescription { get; set; }
    }
}
