using LibraryLogging;
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
        ILogger<CompanyController> loggger;
        public CompanyController(ILogger<CompanyController> _loggger,
            ICompanyBusinessProcess _companyBusinessProcess)
        {
            {
                loggger = _loggger; companyBusinessProcess = _companyBusinessProcess;
            }
        }
        public IActionResult ChangeCompany(CompanyBusinessObject companyBusinessObject)
        {
            return Ok(companyBusinessProcess.ChangeProcess(companyBusinessObject));
        }

    }
}
