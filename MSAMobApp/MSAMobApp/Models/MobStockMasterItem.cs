using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    /// <summary>
    /// Table for storing sample barcode
    /// </summary>
  public  class MobStockMasterItem
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        [Indexed]
        public string BarCode { get; set; }
        public string Number { get; set; }

        public string Unit { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DataState { get; set; } //New/Edit/Posted
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string UserID { get; set; } //User login
        public string HID { get; set; } //HardWare ID
        public DateTime SyncDate { get; set; } //Ngay sync len Server
        public string GLocation { get; set; } //Google location
    }
}
