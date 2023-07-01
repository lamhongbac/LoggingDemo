using DEMOService;
using DEMOService.Configuration;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration configuration;
        AppConfiguration appConfiguraiton;
        private readonly IOptionsMonitor<AppConfiguration> _optionsMonitor;
        public HomeController(ILogger<HomeController> logger,
            IConfiguration configuration, 
            AppConfiguration appConfiguraiton,
           IOptionsMonitor<AppConfiguration> optionsMonitor)
        {
            _logger = logger;
            this.configuration = configuration;
            this.appConfiguraiton = appConfiguraiton;
            _optionsMonitor= optionsMonitor;
        }
        DemoService demoService = null;
        DemoService demoService_old = null;
        public IActionResult Index()
        {
           string companycode1=appConfiguraiton.CompanyCode;
            var sectionConfig = configuration.GetSection("AppConfig");
            string companycode2 = sectionConfig["CompanyCode"].ToLower();
            AppConfiguration update_appConfiguraiton= _optionsMonitor.CurrentValue;
            string companyCode3 = update_appConfiguraiton.CompanyCode;
            demoService_old= DemoService.GetInstance(update_appConfiguraiton);
            demoService = DemoService.GetInstance(update_appConfiguraiton,true);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
