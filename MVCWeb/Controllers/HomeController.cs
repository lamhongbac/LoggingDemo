using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCWeb.Models;
using MVCWeb.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    /// <summary>
    /// https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            APIServices service = new APIServices();
            string result =await service.GetWeather();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
