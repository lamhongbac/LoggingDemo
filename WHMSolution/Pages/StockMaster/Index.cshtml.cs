using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using WHMSolution.Models;

namespace WHMSolution.Pages.StockMaster
{
    public class IndexModel : PageModel
    {
        public IndexModel():base()
        {
            StockMasters = new List<MobMasterStockModel>();
        }
        public void OnGet()
        {
             GetAllStockMaser();
        }

        private void GetAllStockMaser()
        {
            MobMasterStockModel item1 = new MobMasterStockModel()
            {

            };
            MobMasterStockModel item2 = new MobMasterStockModel()
            {

            };
            StockMasters.Add(item1);
            StockMasters.Add(item2);
           // return 
        }       
        public  List<MobMasterStockModel> StockMasters { get; set; }


        


    
    }
   
}
