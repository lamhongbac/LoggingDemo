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
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
    }
}
