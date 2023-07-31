using DynamicRoute.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicRoute.Controllers
{
    public class OrderController : Controller
    {
        [Route("")]
        [Route("Order")]
        [Route("Order/Index")]
        public IActionResult Index(string lang)
        {
            List<ProductViewModel> products = GetProductViewModel();
            return View(products);
            
        }
        private List<ProductViewModel> GetProductViewModel()
        {
            List<ProductViewModel> productModels = new List<ProductViewModel>();
            ProductViewModel model1 = new ProductViewModel()
            {
                ID = 1,
                Code = "Code1",
                Name = "Product 1"
            };
            ProductViewModel model2 = new ProductViewModel()
            {
                ID = 2,
                Code = "Code2",
                Name = "Product 2"
            };
            productModels.Add(model1);
            productModels.Add(model2);
            return productModels;

        }
        public ProductViewModel GetNews(string lang,int id)
        {
            ProductViewModel model2 = new ProductViewModel()
            {
                ID = id,
                Code = "Code" + id,
                Name = "Product " + id
            };

            return model2;
        }
        public ProductViewModel GetProduct(string code)
        {
            ProductViewModel model2 = new ProductViewModel()
            {
                ID = 1,
                Code = code,
                Name = "Product " + 1,
                Data_alias = "Mr Bac"
            };

            return model2;
        }
        

        public IActionResult Detail(string lang,string number)
        {
            ProductViewModel model = GetProduct(number);
            return View(model);
        }
    }
}
