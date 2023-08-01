

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;


using System;
using System.Threading.Tasks;
using DynamicRoute.Models;

namespace DynamicRoute.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IWebHostEnvironment _env;
        ILogger<ErrorController> _logger;
        public ErrorController(IWebHostEnvironment env, ILogger<ErrorController> logger)
        {
            _env = env; _logger = logger;
        }
        // trong action result catch ()
        // chi can thuc hien lenh 
        // throw new exception (message)

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error(string errorMessage= "Exception has occured")
        {
            var exceptionDetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ErrorViewModel model = new ErrorViewModel();
            if (exceptionDetail != null)
            {
               
                errorMessage = string.Format("Eception Path:{0}; exception Message{1}", exceptionDetail.Path,
                  exceptionDetail.Error.Message);
                _logger.LogError(errorMessage, exceptionDetail.Error.StackTrace);

                model.ErrorMessage = errorMessage;

            }
            else
            {
                _logger.LogError(errorMessage);
            }
            
            return View("Error",model);
        }
       

        [Route("Error/{code}")]
        public IActionResult NoteFound(int statusCode)
        {
          
            ErrorViewModel model = new ErrorViewModel();
            
                string errorMessage = "Resource you requested could not be found";
              
                ViewBag.ErrorMessage = errorMessage;

                model.ErrorMessage = errorMessage;




            
            return View("Error", model);
        }

    }
}
