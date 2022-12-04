using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class ReViewNewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
