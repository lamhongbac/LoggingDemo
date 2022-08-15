using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    public class SyncMobStockItemResult
    {
        public DateTime LastSyncDate { get; set; }
        //item update from mobile to server
        public List<MobStockMasterItem> ForMobileUpdate { get; set; }
    }
}
