using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace SCMDAL.DTO
{
    [Table("mssInvTrans")]
    public class MobStockTrans
    {
        public MobStockTrans()
        {
            ID = Guid.NewGuid(); 
            StockTransDetails = new List<StockTransDetail>();
        }
        public Guid ID { get; set; }
        public string Number { get; set; } //so chung tu
        public string UserID { get; set; } //login user
        public string TCode { get; set; } //WHM transaction Code
        public string Description { get; set; } //Notes
        public string StoreNumber { get; set; } //Ma kho or Ma WH
        public string ShelfCode { get; set; } //Ma ke
        public DateTime TransDate { get; set; }
        public DateTime SyncDate { get; set; } //Ngay sync len Server
       
        public string DataState { get; set; }

        public string HID { get; set; } //HardWare ID       
        public string GLocation { get; set; } //Google location
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public List<StockTransDetail> StockTransDetails { get; set; }
    }

    [Table("StockTransDetails")]
    public class StockTransDetail
    {
        public StockTransDetail(Guid parentID)
        {
            TransID = parentID;
            ID = Guid.NewGuid(); Quantity = 1;
        }
        public Guid ID { get; set; }


        public Guid TransID { get; set; } //khoa ngoai

        public string BarCode { get; set; } //dung ra la phai chi dinh khoa ngoai vao bang master stock Item
        public string ItemNumber { get; set; } //ma item
        
        public DateTime ScanDateTimes { get; set; }
        public int Quantity { get; set; }
        public string DataState { get; set; } //data state de du tru cho viec thay doi tren 1  dòng
        public DateTime CreatedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public DateTime ModifiedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public string CreatedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public string ModifiedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
    }
}
