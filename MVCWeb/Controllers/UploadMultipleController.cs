using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class UploadMultipleController : Controller
    {
        private IHostingEnvironment _environment;
        public UploadMultipleController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {

            foreach (IFormFile item in files) { 
            
                string filename=ContentDispositionHeaderValue.
                    Parse(item.ContentDisposition).FileName.Trim('"');
                filename = EnsureFileName(filename);

                using (FileStream stream=System.IO.File.Create(GetPath(filename)))
                {

                }

            }
            return this.Content("sucess");
        }

        private string GetPath(string filename)
        {
            string path = _environment.WebRootPath+"\\Upload\\Images\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path+filename;
        }


        private string EnsureFileName(string filename)
        {
            if (filename.Contains("\\")) 
            {
                filename=filename.Substring(filename.IndexOf("\\")+1);
            }
            return filename;
        }
    }
}
