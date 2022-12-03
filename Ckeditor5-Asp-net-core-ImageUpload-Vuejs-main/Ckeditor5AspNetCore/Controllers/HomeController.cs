using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ckeditor5AspNetCore.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using NewsCMS.Data;
using AutoMapper;

namespace Ckeditor5AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration _configuration;
        IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, 
            IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Home/UploadCKEditorImage")]
        public JsonResult UploadCKEditorImage()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                var rError = new
                {
                    uploaded = false,
                    url = string.Empty
                };
                return Json(rError);
            }

            var formFile = files[0];
            var upFileName = formFile.FileName;
            
            var fileName = Path.GetFileNameWithoutExtension(upFileName) + 
                DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(upFileName);

            var saveDir = @".\wwwroot\upload\";
            var savePath = saveDir + fileName;
            var previewPath = "/upload/" + fileName;

            bool result = true;
            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                using (FileStream fs = System.IO.File.Create(savePath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }

                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result = false;
            }
            var rUpload = new
            {
                uploaded = result,
                url = result ? previewPath : string.Empty
            };
            return Json(rUpload);
        }

        [HttpPost]
        //[AllowMultipleButton(Name = "action", Argument = "ExportToExcel")]
        public async Task<IActionResult> Save(NewsModel model)
        {
            string conn = _configuration.GetConnectionString("DevDB");
            DataOperation dataOP = new DataOperation(conn);
            NewsData data = _mapper.Map<NewsData>(model);

          long result=  await dataOP.InsertData(data);
            if (result > -1)
            {
                return View("Index");
            }
            else
            {
                return View("Review", model);
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            NewsModel model = new NewsModel();
            model.Content = "Hi Lam Hong Bac - good night and good job";
            return View(model);
        }
        public IActionResult Review(NewsModel model)
        {
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}
