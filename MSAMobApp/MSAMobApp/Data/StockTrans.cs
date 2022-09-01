using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.DataBase
{

    /// <summary>
    /// table chua noi dung qet bar code cho cac giao dich
    /// stock receive (SR), stock transfer(STI/STO), stock issue (SI), Waste (SW), StockCount (kiem ke)
    /// data 1 lan quet barcode tren thiet bi
    /// </summary>
   [Table("StockTrans")]
    public class StockTrans
    {
        
        [PrimaryKey]
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
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<StockTransDetail> StockTransDetails { get; set; }
    }

    public class StockTransData
    {
        //DocNo,Notes,UserID,BarCode,ItemNumber,[Name],Unit,Quantity
        public string DocNo { get; set; }
        public string Notes { get; set; }
        public string UserID { get; set; }
        public string BarCode { get; set; }
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
    }
}
