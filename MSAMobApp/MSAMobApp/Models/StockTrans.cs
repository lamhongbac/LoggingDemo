using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    /// <summary>
    /// table chua noi dung qet bar code cho cac giao dich
    /// stock receive (SR), stock transfer(STI/STO), stock issue (SI), Waste (SW), StockCount (kiem ke)
    /// data 1 lan quet barcode tren thiet bi
    /// </summary>
    public class StockTrans
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string Number { get; set; } //so chung tu
        public string UserID { get; set; } //login user
        public string TCode { get; set; } //transaction Code
        //public string Direction { get; set; } //In/Out
        public string StoreNumber { get; set; } //Ma kho or Ma WH
        public string ShelfCode { get; set; } //Ma ke
        
        //public string BarCode { get; set; }
        //public DateTime ScanDateTimes { get; set; }
        //public int Quantity { get; set; }
        public string DataState { get; set; }  
        //theo doi trang thai da dua len Central DB hay chua
        //New: chua dua len=> Posted
        //New->edit--> posted
        //neu da POSTED: thi kg the sua
        
        //New/Posted/Edit                                              
        //khi handhelp quet barcode thi data status = new                                              
        //khi push data len BackEnd thanh cong thi tra ve danh sach barcode voi trang thai la true/false                                              
        //can cu vao do de update vao local DB= Posted                                              
        //khi hand-helo sua data , data chuyen trang thai POSTED=>Edit (New thi kg co thay doi)
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
