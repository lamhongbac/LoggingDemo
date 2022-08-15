using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    public class SyncStockItemModel
    {
        /// <summary>
        /// client sycn date
        /// </summary>
        public DateTime LastSyncDate { get; set; }
        //item update from mobile to server
        public List<MobStockMasterItem> MobItems;
    }
}
