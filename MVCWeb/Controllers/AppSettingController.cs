using Microsoft.AspNetCore.Mvc;
using MVCWeb.Helper;
using MVCWeb.Models;

namespace MVCWeb.Controllers
{
    public class AppSettingController : Controller
    {
        AppSettingViewModelHelper appSettingHelper;
        public AppSettingController(AppSettingViewModelHelper appSettingHelper)
        {

            this.appSettingHelper = appSettingHelper;   

        }
        public IActionResult Index()
        {
          var items=  appSettingHelper.GetItems();
            return View(items);
        }
        public IActionResult Edit(string id)
        {
           
            AppSettingViewModel model = appSettingHelper.GetItem(id);

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AppSettingViewModel model)
        {
            appSettingHelper.Update(model);

            return RedirectToAction("Index");
        }
    }
}
