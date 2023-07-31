using DynamicRoute.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicRoute.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index(string lang)
        {
            List<NewsViewModel> newsModels = GetNewsViewModel();
            return View(newsModels);
            
        }
        private List<NewsViewModel> GetNewsViewModel()
        {
            List<NewsViewModel> newsModels = new List<NewsViewModel>();
            NewsViewModel model1 = new NewsViewModel()
            {
                ID = 1,
                Content = "Content 1",
                Subject = "Subject 1"
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
        public NewsViewModel GetNews(string lang,int id)
        {
            NewsViewModel model2 = new NewsViewModel()
            {
                ID = id,
                Content = "Content " + id,
                Subject = "Subject " + id
            };

            return model2;
        }
        public IActionResult Detail(string lang, int Id)
        {
            NewsViewModel model = GetNews(lang,Id);
            return View(model);
        }
    }
}
