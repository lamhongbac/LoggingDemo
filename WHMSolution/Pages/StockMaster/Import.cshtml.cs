using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WHMSolution.Pages.StockMaster
{

    public class ImportModel : PageModel
    {
        private IHostingEnvironment _hostingEnv;
        public ImportModel(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }
        public void OnGet()
        {
        }
        public FileViewModel FileUpload { get; set; }
        public async Task<ActionResult> OnPostAsync()
        {
            if (FileUpload.FormFile.Length > 0)
            {

            }

        }
    }
    public class FileViewModel
    {
        public IFormFile FormFile { get; set; }
    }
}
