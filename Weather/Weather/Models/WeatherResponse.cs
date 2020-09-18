using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Rain
    {
        [JsonProperty("1h")]
        public double H1 { get; set; }
        [JsonProperty("3h")]
        public double H3 { get; set; }
    }

    public class Snow
    {
        [JsonProperty("1h")]
        public double H1 { get; set; }
        [JsonProperty("3h")]
        public double H3 { get; set; }
    }

    public class Clouds
    {
        public double all { get; set; }
    }

    public class Root
    {
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public double visibility { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Snow snow { get; set; }
        public Clouds clouds { get; set; }
        public long dt { get; set; }
        public double id { get; set; }
        public string name { get; set; }
    }
}
