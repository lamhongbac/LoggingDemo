using Microsoft.AspNetCore.Mvc;

namespace MVCWeb.Controllers
{
    public class TimeAttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
