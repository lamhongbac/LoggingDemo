using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWeb.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class FileUploadController : Controller
    {
        private IHostingEnvironment _environment;
        public FileUploadController(IHostingEnvironment environment)
        {
                _environment = environment; 
        }
        public IActionResult Index()
        {
            UploadFileViewModel viewModel=new UploadFileViewModel();
            return View();
        }



        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(UploadFileViewModel viewModel)
        {
            IFormFile[] files = viewModel.FileUploads;
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\upload\\images", formFile.FileName);
                    //var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    //filePaths.Add(filePath);

                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count(), size, filePaths });
        }
    }
}

