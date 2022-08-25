using MSAMobApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.ViewModels
{
  public  class StockTransItemViewModel: BaseViewModel
    {
        
        public Guid ID { get; set; }

        
        public Guid StockTransID { get; set; } //khoa ngoai

        public string BarCode { get; set; } //dung ra la phai chi dinh khoa ngoai vao bang master stock Item
        public string Number { get; set; } //ma item
        public string Name { get; set; } //ten Item
        public string Unit { get; set; } //DVT Item
        public DateTime ScanDateTimes { get; set; }
        public int Quantity { get; set; }
        public string DataState { get; set; } //data state de du tru cho viec thay doi tren 1  dòng
        public DateTime CreatedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public DateTime ModifiedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public string CreatedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public string ModifiedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public StockTransItemViewModel(StockTransDetail detailItem, string name, string unit)
        {
            ID = detailItem.ID;
            StockTransID = detailItem.TransID;
            BarCode = detailItem.BarCode;
            Quantity = detailItem.Quantity;
            Number = detailItem.ItemNumber;
            CreatedBy = detailItem.CreatedBy;
            ModifiedBy = detailItem.ModifiedBy;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
            DataState = EDataState.New.ToString();
            Name = name;
            Unit = unit;

        }
    }
}
