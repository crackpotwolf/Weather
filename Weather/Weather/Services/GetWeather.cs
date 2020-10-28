using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services
{
    public class GetWeather : IHostedService, IDisposable
    {
        private readonly string url = "http://api.openweathermap.org/data/2.5/weather?id=472757&lang=ru&units=metric&appid=ebc8fa7b52bf9e3a234f957bf2cd405e";
        private Timer _timer_weather;

        private readonly ILogger _logger;
        private readonly IServiceProvider _scopeFactory;

        public GetWeather(IServiceProvider scopeFactory, ILogger<GetWeather> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public void Dispose()
        {

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            TimerCallback get_weather = new TimerCallback(GetWeathers);

            // Получение данных о погоде
            _timer_weather = new Timer(
                callback: get_weather,
                state: null,
                dueTime: TimeSpan.FromSeconds(30),
                period: TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Получение данных о погоде,и запись в бд
        /// </summary>
        /// <param name="state"></param>
        public void GetWeathers(object state)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                using (var db = scope.ServiceProvider.GetRequiredService<WeatherContext>())
                {
                    _logger.LogInformation("Отправка запроса на полученние данных...");
                    
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    string weather_json;
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        weather_json = streamReader.ReadToEnd();

                    Root weather_response = JsonConvert.DeserializeObject<Root>(weather_json);
                    DateTime datatime = DateTime.UtcNow.AddMinutes(-DateTime.UtcNow.Minute)
                        .AddSeconds(-DateTime.UtcNow.Second)
                        .AddHours(4);

                    if(DateTime.UtcNow.Minute >= 30)
                        datatime = datatime.AddMinutes(30);

                    if (db.WeatherMain.Where(p => p.DateTime.Hour == datatime.Hour && 
                        p.DateTime.Minute == datatime.Minute).FirstOrDefault() == null)
                    {
                        WeatherMain weather = new WeatherMain
                        {
                            Name = weather_response.name,
                            DateTime = datatime,
                            Main = weather_response.weather.FirstOrDefault().main,
                            Description = weather_response.weather.FirstOrDefault().description,
                            Temp = weather_response.main.temp,
                            TempMax = weather_response.main.temp_max,
                            TempMin = weather_response.main.temp_min,
                            FeelsLike = weather_response.main.feels_like,
                            Pressure = weather_response.main.pressure,
                            Humidity = weather_response.main.humidity,
                            WindSpeed = weather_response.wind.speed,
                            WindDeg = weather_response.wind.deg,
                            Clouds = weather_response.clouds.all,
                            OriginalJson = weather_json
                        };

                        if (weather_response.rain != null)
                        {
                            weather.Rain1h = weather_response.rain.H1;
                            weather.Rain3h = weather_response.rain.H3;
                        }
                        if (weather_response.snow != null)
                        {
                            weather.Snow1h = weather_response.snow.H1;
                            weather.Snow3h = weather_response.snow.H3;
                        }

                        db.WeatherMain.Add(weather);
                        db.SaveChanges();

                        _logger.LogInformation("Данные успешно сохранены");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ошибка получение данных: {ex.Message}");
                Console.WriteLine($"Ошибка получение данных: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}