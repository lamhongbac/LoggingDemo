using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using SCMDAL.DataHandler;
using SCMDAL.DTO;
using WHMSolution.Models;

namespace WHMSolution.Pages.StockMaster
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _hostenvironment;

        WHMApplication _application;
        public IndexModel(IWebHostEnvironment hostingEnv, WHMApplication application) :base()
        {
            _hostenvironment = hostingEnv; 
            _application = application;
            StockMasters = new List<MobStockMasterItem>();
        }
        public async Task OnGet()
        {
           await  GetAllStockMaser();
        }

        private async Task GetAllStockMaser()
        {
            //MobStockMasterItem item1 = new MobStockMasterItem()
            //{

            //};
            //MobStockMasterItem item2 = new MobStockMasterItem()
            //{

            //};
            //StockMasters.Add(item1);
            //StockMasters.Add(item2);
            // return=
            MobStockMasterHandler handler = _application.StockMasterDataBase;
            //string whereCondition, object para

            string whereCondition = string.Empty;
            StockMasters =await handler.GetItems(whereCondition,null);
        }       
        public  List<MobStockMasterItem> StockMasters { get; set; }


        


    
    }
   
}
