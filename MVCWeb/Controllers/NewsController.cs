using Microsoft.AspNetCore.Mvc;
using MVCWeb.Models;
using System;
using System.Collections.Generic;

namespace MVCWeb.Controllers
{
    public class NewsController : Controller
    {
        
        public IActionResult Index()
        {
            List<NewsViewModel> newsModels = GetNewsViewModel();
            return View(newsModels);
        }

        private List<NewsViewModel> GetNewsViewModel()
        {
            List<NewsViewModel> newsModels=new List<NewsViewModel>();
            NewsViewModel model1 = new NewsViewModel()
            {
                 ID=1,
                  Content="Content 1",
                   Subject="Subject 1"
            };
            NewsViewModel model2 = new NewsViewModel()
            {
                ID = 2,
                Content = "Content 2",
                Subject = "Subject 2"
            };
            newsModels.Add(model1);
            newsModels.Add(model2);
            return newsModels;

        }
        public NewsViewModel GetNews(int id)
        {
            NewsViewModel model2 = new NewsViewModel()
            {
                ID = id,
                Content = "Content "+id,
                Subject = "Subject "+id
            };

            return model2;
        }
        public IActionResult Detail(int Id)
        {
            NewsViewModel model = GetNews(Id);
            return View(model);
        }
    }
}
