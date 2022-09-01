using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Services
{
    
    public class GetStockTransModel
    {
           
        public string StoreNumber { get; set; } //Ma kho or Ma WH        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; } //Ngay sync len Server

    }
}
