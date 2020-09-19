using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weather.Models;

namespace Weather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAPI : ControllerBase
    {
        WeatherContext _db;

        public WeatherAPI(WeatherContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Предоставление погодных данных за промежуток времени
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeather")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeatherVM), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetWeather(DateTime start, DateTime end) 
        {
            try
            {
                var weather = _db.WeatherMain.Where(p => p.DateTime >= start && p.DateTime <= end).ToList();
                var weather_view = new List<WeatherVM>();

                foreach (var item in weather)
                {
                    weather_view.Add(new WeatherVM()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DateTime = item.DateTime,
                        Main = item.Main,
                        Description = item.Description,
                        Temp = item.Temp,
                        TempMax = item.TempMax,
                        TempMin = item.TempMin,
                        FeelsLike = item.FeelsLike,
                        Pressure = item.Pressure,
                        Humidity = item.Humidity,
                        WindSpeed = item.WindSpeed,
                        WindDeg = item.WindDeg,
                        Clouds = item.Clouds,
                        Rain1h = item.Rain1h,
                        Rain3h = item.Rain3h,
                        Snow1h = item.Snow1h,
                        Snow3h = item.Snow3h
                    });
                }
                return Ok(weather_view);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Предоставление всех погодных данных
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllWeather")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeatherVM), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetAllWeather()
        {
            try
            {
                var weather = _db.WeatherMain.ToList();
                var weather_view = new List<WeatherVM>();

                foreach (var item in weather)
                {
                    weather_view.Add(new WeatherVM()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DateTime = item.DateTime,
                        Main = item.Main,
                        Description = item.Description,
                        Temp = item.Temp,
                        TempMax = item.TempMax,
                        TempMin = item.TempMin,
                        FeelsLike = item.FeelsLike,
                        Pressure = item.Pressure,
                        Humidity = item.Humidity,
                        WindSpeed = item.WindSpeed,
                        WindDeg = item.WindDeg,
                        Clouds = item.Clouds,
                        Rain1h = item.Rain1h,
                        Rain3h = item.Rain3h,
                        Snow1h = item.Snow1h,
                        Snow3h = item.Snow3h
                    });
                }
                return Ok(weather_view);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}