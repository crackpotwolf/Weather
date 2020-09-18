using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class WeatherMain
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public double Temp { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public double FeelsLike { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindDeg { get; set; }
        public double Clouds { get; set; }
        public double Rain1h { get; set; }
        public double Rain3h { get; set; }
        public double Snow1h { get; set; }
        public double Snow3h { get; set; }
        public string OriginalJson { get; set; }
    }
}
