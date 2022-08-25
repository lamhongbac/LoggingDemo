using SCMDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMAPI.Models
{
    public class SyncMobStockItemResult
    {
        public DateTime LastSyncDate { get; set; }
        //item update from mobile to server
        public List<MobStockMasterItem> ForMobileUpdate { get; set; }
    }

    public class SyncMobStockTransResult
    {
        public DateTime LastSyncDate { get; set; }
        //item update from mobile to server
        public List<MobStockTrans> ForMobileUpdate { get; set; }
    }
}
