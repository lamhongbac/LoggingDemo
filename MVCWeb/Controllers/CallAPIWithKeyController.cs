using Microsoft.AspNetCore.Mvc;
using MVCWeb.Services;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{/// <summary>
 /// https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/
 /// </summary>
    public class CallAPIWithKeyController : Controller
    {
        public async Task<IActionResult> Index()
        {
            APIServices service = new APIServices();
            string result = await service.GetWeather();
            return View();
        }
    }
}
