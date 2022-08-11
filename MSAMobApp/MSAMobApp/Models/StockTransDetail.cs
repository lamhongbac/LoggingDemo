using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace MSAMobApp.Models
{
  public  class StockTransDetail
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string BarCode { get; set; }
        public DateTime ScanDateTimes { get; set; }
        public int Quantity { get; set; }
        public string DataState { get; set; }
    }
}
