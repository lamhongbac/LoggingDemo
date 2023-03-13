using Microsoft.AspNetCore.Mvc;
using MVCWeb.Helper.ChatGpt;
using MVCWeb.Models;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class ChatGPTController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatViewModel model)
        {

            string question = model.MyQuestion;
            ChatGptUtil chatGptUtil = new ChatGptUtil();
           string answer=await chatGptUtil.ChatWithGpt(question);
            model.Answer = answer;
            return View(model);
        }
    }
}
