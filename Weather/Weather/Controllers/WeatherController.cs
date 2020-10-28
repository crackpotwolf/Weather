using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Weather.Extensions;
using Weather.Models;

namespace Weather.Controllers
{
    public class WeatherController : Controller
    {

        WeatherContext _db;

        public WeatherController(WeatherContext context)
        {
            _db = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public virtual IActionResult WeatherView()
        {
            return PartialView("_WeatherPartial"); 
        }

        public List<WeatherInfo> GetWeather(string start_date, string end_date)
        {
            var weather_view = new List<WeatherInfo>();

            try
            {
                DateTime start = DateTime.ParseExact(start_date,
                  "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                DateTime end = DateTime.ParseExact(end_date,
                      "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                var weather = _db.WeatherMain.Where(p => p.DateTime >= start && p.DateTime <= end).ToList();
               
                foreach (var item in weather)
                {
                    weather_view.Add(new WeatherInfo()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Date = item.DateTime.DateTimeToString("dd.MM.yyyy"),
                        Time = item.DateTime.DateTimeToString("HH:mm:ss"),
                        Description = item.Description,
                        Temp = item.Temp,
                        FeelsLike = item.FeelsLike,
                        Pressure = item.Pressure,
                        Humidity = item.Humidity,
                        WindSpeed = item.WindSpeed,
                        Clouds = item.Clouds,
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получение погодных данных: {ex.Message}");
            }

            return weather_view;
        }
    }
}
