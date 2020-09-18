using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather
{
    public class WeatherContext : DbContext
    {
        public virtual DbSet<WeatherRequest> WeatherRequests { get; set; }

        public WeatherContext()
        { }

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {

        }
    }
}
