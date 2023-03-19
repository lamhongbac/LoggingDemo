using Microsoft.AspNetCore.Mvc;
using MVCWeb.Models;

namespace MVCWeb.Controllers
{
    public class MultipleCheckBoxController : Controller
    {
        public IActionResult Index()
        {
            MutipleCheckBoxModel model = new MutipleCheckBoxModel()
            {
                CheckBoxes = new System.Collections.Generic.List<CheckBoxOption>()
                {
                    new CheckBoxOption()
                    {
                         IsBlogActive= true,
                          Description="option1",
                           Name="checkBox1",
                            Value="option1"
                    },
                    new CheckBoxOption()
                    {
                         IsBlogActive= false,
                          Description="option 2",
                           Name="checkBox2",
                            Value="option2"
                    },
                    new CheckBoxOption()
                    {
                         IsBlogActive= false,
                          Description="option 3",
                           Name="checkBox1",
                            Value="option3"
                    }
                }

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(MutipleCheckBoxModel model)
        {
            //return View(model);
            return RedirectToAction("Index");
        }
    }
}
