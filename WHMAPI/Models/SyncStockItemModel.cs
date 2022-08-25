using SCMDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMAPI.Models
{
    /// <summary>
    /// Sync Stock Item voi client (mob,web)
    /// </summary>
    public class SyncStockItemModel
    {
        /// <summary>
        /// client sycn date
        /// </summary>
        public DateTime LastSyncDate { get; set; }
        //item update from mobile to server
        public List<MobStockMasterItem> MobItems;
    }

    public class SyncStockTransModel
    {
        /// <summary>
        /// client sycn date
        /// </summary>
        public DateTime LastSyncDate { get; set; }
        //item update from mobile to server
        public List<MobStockTrans> MobItems;
    }
}
