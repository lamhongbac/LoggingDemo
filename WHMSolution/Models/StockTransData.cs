using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSolution.Models
{
    /// <summary>
    /// lop dung de lay data dua ra excel
    /// </summary>
    public class StockTransData
    {
        //DocNo,Notes,UserID,BarCode,ItemNumber,[Name],Unit,Quantity
        public string DocNo { get; set; }
        public string Notes { get; set; }
        public string UserID { get; set; }
        public string BarCode { get; set; }
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
    }
}
