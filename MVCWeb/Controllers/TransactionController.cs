using Microsoft.AspNetCore.Mvc;
using MVCWeb.Helper.PopUp;
using MVCWeb.Models;
using System.Collections.Generic;

namespace MVCWeb.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            DemoData demoData = new DemoData();
            List<TransactionViewModel> model = demoData.CreateListData();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TransactionViewModel model)
        {
            return View(model);
        }
    }
}
