using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WHMSolution.Pages.StockTrans
{
    public class StockTransDetailModel : PageModel
    {
        [BindProperty]
        public DateTime FromDate { get; set; }
        [BindProperty]
        public DateTime ToDate { get; set; }
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
        public void OnPostAsync()
        {
            //handle button submit
            var beginDate = Request.Form["beginDate"];
        }
    }
}
