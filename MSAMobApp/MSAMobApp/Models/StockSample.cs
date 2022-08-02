using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    /// <summary>
    /// Table for storing sample barcode
    /// </summary>
  public  class StockSample
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string BarCode { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string DataState { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
