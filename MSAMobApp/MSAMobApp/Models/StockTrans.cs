using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    /// <summary>
    /// data 1 lan quet barcode tren thiet bi
    /// </summary>
    public class StockTrans
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string UserID { get; set; } //login user
        public string Direction { get; set; } //In/Out
        public string WHCode { get; set; } //Ma kh0
        public string SelfCode { get; set; } //Ma ke
        public string BarCode { get; set; }
        public DateTime ScanDateTimes { get; set; }
        public int Quantity { get; set; }
        public string DataStatus { get; set; } //New/Posted/Edit
        //khi handhelp quet barcode thi data status = new
        //khi push data len BackEnd thanh cong thi tra ve danh sach barcode voi trang thai la true/false
        //can cu vao do de update vao local DB= Posted
        //khi hand-helo sua data , data chuyen trang thai POSTED=>Edit (New thi kg co thay doi)
    }
}
