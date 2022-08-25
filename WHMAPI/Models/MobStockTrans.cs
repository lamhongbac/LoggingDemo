//using Dapper.Contrib.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WHMAPI.Models
//{
//    [Table("StockTrans")]
//    public class MobStockTrans
//    {
       
//        public Guid ID { get; set; }
//        public string Number { get; set; } //so chung tu
//        public string UserID { get; set; } //login user
//        public string TCode { get; set; } //WHM transaction Code
//        public string Description { get; set; } //Notes
//        public string StoreNumber { get; set; } //Ma kho or Ma WH
//        public string ShelfCode { get; set; } //Ma ke
//        public DateTime TransDate { get; set; }
//        public DateTime SyncDate { get; set; } //Ngay sync len Server
//        //public string BarCode { get; set; }
//        //public DateTime ScanDateTimes { get; set; }
//        //public int Quantity { get; set; }
//        public string DataState { get; set; }
//        //theo doi trang thai da dua len Central DB hay chua
//        //New: chua dua len=> Posted
//        //New->edit--> posted
//        //neu da POSTED: thi kg the sua

//        //New/Posted/Edit                                              
//        //khi handhelp quet barcode thi data status = new                                              
//        //khi push data len BackEnd thanh cong thi tra ve danh sach barcode voi trang thai la true/false                                              
//        //can cu vao do de update vao local DB= Posted                                              
//        //khi hand-helo sua data , data chuyen trang thai POSTED=>Edit (New thi kg co thay doi)
//        public DateTime CreatedOn { get; set; }
//        public DateTime ModifiedOn { get; set; }
//        public string CreatedBy { get; set; }
//        public string ModifiedBy { get; set; }
        
//        public List<StockTransDetail> StockTransDetails { get; set; }
//    }

//    [Table("StockTransDetails")]
//    public class StockTransDetail
//    {
        
//        public Guid ID { get; set; }

       
//        public Guid TransID { get; set; } //khoa ngoai

//        public string BarCode { get; set; } //dung ra la phai chi dinh khoa ngoai vao bang master stock Item
//        public string ItemNumber { get; set; } //ma item
//        //public string Name { get; set; } //ten Item
//        //public string Unit { get; set; } //DVT Item
//        public DateTime ScanDateTimes { get; set; }
//        public int Quantity { get; set; }
//        public string DataState { get; set; } //data state de du tru cho viec thay doi tren 1  dòng
//        public DateTime CreatedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
//        public DateTime ModifiedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
//        public string CreatedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
//        public string ModifiedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
//    }
//}
