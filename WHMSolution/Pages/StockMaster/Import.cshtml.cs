using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WHMSolution.Models;


namespace WHMSolution.Pages.StockMaster
{

    public class ImportModel : PageModel
    {
      
        //public string UserID { get; set; }
        private IWebHostEnvironment _hostenvironment;
        
        WHMApplication _application;
        public ImportModel(IWebHostEnvironment hostingEnv, WHMApplication application)
        {
            _hostenvironment = hostingEnv; FileUpload = new FileViewModel();
            _application = application;
            
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public FileViewModel FileUpload { get; set; }

        public async Task<ActionResult> OnPostAsync()
        {
            bool isSuccess = false;
            Utilities appUtil = _application.AppUtil;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //upload file to folder
            if (FileUpload.FormFile.Length > 0)
            {
                isSuccess= await appUtil.ImportMasterItemBarCode(FileUpload.FormFile);

            }
            //save image to database.
            if (!isSuccess)
            {
                //thong bao cho giao dien la import kg thanh cong
                //ly do la gi?
            }
            return RedirectToPage("./Index");

        }
    }
    
}
