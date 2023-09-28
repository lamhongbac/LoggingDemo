using Microsoft.AspNetCore.Mvc;

namespace MVCWeb.Controllers
{
    /// <summary>
    /// cung cap cac method su dung nhu 1 thu vien tien ich
    /// Utililties method
    /// </summary>
    public class MVCUtilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
