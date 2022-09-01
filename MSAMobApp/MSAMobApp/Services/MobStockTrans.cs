
using System;
using System.Collections.Generic;


/// <summary>
/// This for Service dataType
/// </summary>
namespace SCMDAL.DTO
{
   
    public class MobStockTrans
    {
        public MobStockTrans()
        {
            ID = Guid.NewGuid(); 
            StockTransDetails = new List<MobStockTransDetail>();
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

        public List<MobStockTransDetail> StockTransDetails { get; set; }
    }

    
    public class MobStockTransDetail
    {
        //public MobStockTransDetail(Guid parentID)
        //{
        //    TransID = parentID;
        //    ID = Guid.NewGuid(); Quantity = 1;
        //}
        public MobStockTransDetail()
        {
         
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
