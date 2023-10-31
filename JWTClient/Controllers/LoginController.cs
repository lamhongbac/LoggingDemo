using JWTClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index(LoginModel model)
        {
            return View();
        }
    }
}
