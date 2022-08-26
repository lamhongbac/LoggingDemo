using SCMDAL.DataHandler;
using SCMDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMAPI.Models
{
    public class MobSyncHandler
    {
        string _connectionstring;
        public MobSyncHandler(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        /// <summary>
        /// 1.Create Items which scaned by Mobile to Server
        /// 2.Get Items which have modifiedDate>LastsyncDate
        /// if first sync get all items
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SyncMobStockItemResult> MobSyncStockItem(SyncStockItemModel model)
        {
            MobStockMasterHandler dal = new MobStockMasterHandler(_connectionstring);
               SyncMobStockItemResult result = new SyncMobStockItemResult();
            bool isFirstSync = model.LastSyncDate == DateTime.MinValue;
            DateTime serverLastChangeDate = DateTime.MinValue;
            if (dal.GetServerLastChangeDate()!=null)
            {
                serverLastChangeDate = Convert.ToDateTime(dal.GetServerLastChangeDate().Key);
            }
            

            result.LastSyncDate = serverLastChangeDate;

            List<MobStockMasterItem> to_created = new List<MobStockMasterItem>();
            if (model.MobItems != null && model.MobItems.Count > 0)
            {
                to_created = await dal.CreateMobStockItems(model.MobItems);
            }


            List<MobStockMasterItem> syncitems = new List<MobStockMasterItem>();
            syncitems.AddRange(to_created);

            if (isFirstSync)
            {
                syncitems = await dal.GetItems(string.Empty, null);
                result.ForMobileUpdate = syncitems;
                return result;
            }

            DateTime mobLastSyncDate = DateTime.MinValue;
            List<MobStockMasterItem> change_items = new List<MobStockMasterItem>();

            if (serverLastChangeDate > mobLastSyncDate)
            {
                change_items = await dal.GetLastChangeItems(mobLastSyncDate);


                if (change_items.Count > 0)
                {
                    syncitems.AddRange(change_items);
                }
            }

            result.ForMobileUpdate = change_items;

            return result;
        }

    }
}
