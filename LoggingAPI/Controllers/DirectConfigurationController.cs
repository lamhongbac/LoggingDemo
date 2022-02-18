using LoggingAPI.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectConfigController : ControllerBase
    {
        IConfiguration applicationConfig; TestOption testOption;

        public DirectConfigController(IConfiguration _applicationConfig)
        {
            applicationConfig = _applicationConfig;
            testOption =
                applicationConfig.GetSection("TestOption").Get<TestOption>();
        }

        [HttpGet]
        public IActionResult Demo()
        {
           

            return Ok(testOption);
        }
    }
}
