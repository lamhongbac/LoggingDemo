using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WHMSolution.Models;

namespace WHMSolution.Pages.StockTrans
{
    public class StockTransDetailModel : PageModel
    {
        private IWebHostEnvironment _hostenvironment;
        WHMApplication _application;

        public StockTransDetailModel(IWebHostEnvironment hostingEnv, WHMApplication application) :base()
            {
            _hostenvironment = hostingEnv; 
            _application = application;
        }
        [BindProperty]
        public DateTime FromDate { get; set; }
        [BindProperty]
        public DateTime ToDate { get; set; }

        [Display(Name = "Transaction Code")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [BindProperty]
        public string TCode { get; set; }
        [BindProperty]
        public string StoreNo { get; set; }
        public void OnGet()
        {
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            StoreNo = "DemoStore";
            TCode = "SI";//stock Issue
        }
        public StockTransDetailModel()
            {
        }
        public async Task<ActionResult> OnPostAsync()
        {
            //handle button submit
          bool  isSuccess = false;
            Utilities appUtil = _application.AppUtil;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            SQLDataBase database = _application.DataBase;

            List<StockTransData> stockTransList = await database.GetStockTransData(FromDate, ToDate, TCode, StoreNo); ;
            isSuccess= appUtil.ExportToExcel(stockTransList);
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
