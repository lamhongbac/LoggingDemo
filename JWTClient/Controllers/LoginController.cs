using JWTClient.Models;
using JWTClient.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace JWTClient.Controllers
{
    public class LoginController : Controller
    {
        AccountService accountService;
        public LoginController(AccountService accountService) {
            this.accountService = accountService;   }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index(LoginModel model)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            accountService.Login(model);

            return View();
        }
    }
}
