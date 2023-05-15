using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCWeb.Models;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult WebConfigChange()
        {
            var appSettingsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
            var json = System.IO.File.ReadAllText(appSettingsPath);
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ExpandoObjectConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());

            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);
            config.DebugEnabled = true;
            //    "DevConnectionString": "Data Source=115.165.166.2;Initial Catalog=CLMHQ;User ID=dev;Password=1q2w3e4r;TrustServerCertificate=true",
            DBConnectionViewModel vm = config.ConnectionStrings;

            config.ConnectionStrings.DevConnectionString = "Data Source=115.165.166.2;Initial Catalog=CLMHQ1;User ID=dev;Password=1q2w3e4r;TrustServerCertificate=true";
            var newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);
            System.IO.File.WriteAllText(appSettingsPath, newJson);
            //IConfiguration configuration;
            //using (StreamReader sr = new StreamReader(appSettingsPath))
            //{
            //    configuration = JsonConvert.DeserializeObject<IConfiguration>(sr.ReadToEnd());
            //}
            //DBConnectionViewModel vm = configuration.Get<DBConnectionViewModel>();
            return View(vm);

        }
        [HttpPost]
        public IActionResult WebConfigChange(DBConnectionViewModel vm)
        {
            config.ConnectionStrings.DevConnectionString = "Data Source=115.165.166.2;Initial Catalog=CLMHQ1;User ID=dev;Password=1q2w3e4r;TrustServerCertificate=true";
            var newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);
            System.IO.File.WriteAllText(appSettingsPath, newJson);
            return RedirectToAction("Index", "Home");
        }
    }
}
