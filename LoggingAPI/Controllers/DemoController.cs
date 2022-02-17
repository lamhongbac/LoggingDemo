using LoggingAPI.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        TestOption testOption;
        public DemoController(IOptions<TestOption> _testOption)
        {
            testOption = _testOption.Value;
        }
        [HttpGet]
        public IActionResult Demo()
        {
            return Ok(testOption);
        }
    }
}
