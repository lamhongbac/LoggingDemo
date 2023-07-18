using Microsoft.AspNetCore.Mvc;
using MVCWeb.Models;
using System;
using System.Collections.Generic;

namespace MVCWeb.Controllers
{
    public class AliasController : Controller
    {
        
        [Route("Alias")]
        [Route("Alias/Index")]
        public IActionResult Index(string lang, string coverAlias)
        { 
            if (!IsValidLang(lang))
            {
                return View("Error");

            }
            List<AliasDemoModel> models = GetNewsViewModel();
            if (!ValidAlias(coverAlias))
            {
                return View("Error");
            }
            else
            {
                return View(models);
            }
            //return View();
        }
        private List<AliasDemoModel> GetNewsViewModel()
        {
            List<AliasDemoModel> newsModels = new List<AliasDemoModel>();
            AliasDemoModel model1 = new AliasDemoModel()
            {
                ID = 1,
                Content = "Content 1",
                Subject = "Subject 1"
            };
            AliasDemoModel model2 = new AliasDemoModel()
            {
                ID = 2,
                Content = "Content 2",
                Subject = "Subject 2"
            };
            newsModels.Add(model1);
            newsModels.Add(model2);
            return newsModels;

        }
        public AliasDemoModel GetNews(int id)
        {
            AliasDemoModel model2 = new AliasDemoModel()
            {
                ID = id,
                Content = "Content " + id,
                Subject = "Subject " + id
            };

            return model2;
        }
        private bool IsValidLang(string lang)
        {
            return true;
        }

        private bool ValidAlias(string coverAlias)
        {
            return true;
        }

        public IActionResult Detail(int Id)
        {
            AliasDemoModel model = GetNews(Id);
            return View(model);
        }
    }
}
