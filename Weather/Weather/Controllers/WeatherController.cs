using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
