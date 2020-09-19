using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class WeatherInfo
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Clouds { get; set; }
    }
}
