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
    [Route("")]
    [Route("Home")]
    [Route("Home/Index")]
    //AppConfiguration appConfiguraiton,
    //IOptionsMonitor<AppConfiguration> optionsMonitor
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration configuration;
        DemoServiceSecond theSecond;
        //AppConfiguration appConfiguraiton;
        //private readonly IOptionsMonitor<AppConfiguration> _optionsMonitor;
        public HomeController(ILogger<HomeController> logger,
            IConfiguration configuration,DemoService demoService, DemoServiceSecond theSecond
            )
        {
            this.theSecond=theSecond;
            _logger = logger;
            this.configuration = configuration;
            //this.appConfiguraiton = appConfiguraiton;
            //_optionsMonitor= optionsMonitor;
            this.demoService = demoService;
        }
        DemoService demoService = null;
        //DemoService demoService_old = null;
        public IActionResult Index()
        {
           
            var sectionConfig = configuration.GetSection("AppConfig");
           

            
            string companycode2 = sectionConfig["CompanyCode"].ToLower();
            //AppConfiguration update_appConfiguraiton= _optionsMonitor.CurrentValue;
            //demoService = DemoService.GetInstance();
            string companyCode3 = demoService.CompanyCode;
            string companyCode4 = theSecond.CompanyCode;
           //var demoService1 = DemoService.GetInstance();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
