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
using RazorAPP.Data;


namespace WHMSolution.Pages.StockMaster
{

    public class ImportModel : PageModel
    {
        private IHostingEnvironment _hostenvironment;
        public ImportModel(IHostingEnvironment hostingEnv)
        {
            _hostenvironment = hostingEnv;
        }
        public void OnGet()
        {
        }
        public FileViewModel FileUpload { get; set; }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //upload file to folder
            if (FileUpload.FormFile.Length > 0)
            {
                using (var stream = new FileStream(Path.Combine(_hostenvironment.WebRootPath, "uploadfiles", FileUpload.FormFile.FileName), FileMode.Create))
                {
                    await FileUpload.FormFile.CopyToAsync(stream);
                }
            }
            //save image to database.
            using (var memoryStream = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    var file = new AppFile()
                    {
                        FileName = FileUpload.FormFile.FileName,
                        Content = memoryStream.ToArray()
                    };
                    _context.File.Add(file);

                    await _context.SaveChangesAsync();

                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            return RedirectToPage("./Index");

        }
    }
    public class FileViewModel
    {
        public IFormFile FormFile { get; set; }
    }
    public class AppFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
