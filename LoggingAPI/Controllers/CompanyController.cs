using LibraryLogging;
using LibraryLogging.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyBusinessProcess companyBusinessProcess;
        ILogger<CompanyController> logger;
        public CompanyController(ILogger<CompanyController> _loggger,
            ICompanyBusinessProcess _companyBusinessProcess)
        {
            {
                logger = _loggger; 
                companyBusinessProcess = _companyBusinessProcess;
            }
        }
        [HttpPost]
        [Route("ChangeCompany")]
        public IActionResult ChangeCompany(Company companyBusinessObject)
        {
            logger.LogInformation("get request at {0}", DateTime.Now);
            return Ok(companyBusinessProcess.ChangeProcess(companyBusinessObject));
        }

    }
}
