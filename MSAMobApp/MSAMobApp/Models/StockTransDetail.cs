using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MSAMobApp.Models
{
    [Table("StockTransDetails")]
  public  class StockTransDetail
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [ForeignKey(typeof(StockTrans))]
        public Guid StockTransID { get; set; } //khoa ngoai

        public string BarCode { get; set; } //dung ra la phai chi dinh khoa ngoai vao bang master stock Item
        public string ItemNumber { get; set; } //ma item
        //public string Name { get; set; } //ten Item
        //public string Unit { get; set; } //DVT Item
        public DateTime ScanDateTimes { get; set; }
        public int Quantity { get; set; }
        public string DataState { get; set; } //data state de du tru cho viec thay doi tren 1  dòng
        public DateTime CreatedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public DateTime ModifiedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public string CreatedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public string ModifiedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
    }
}
