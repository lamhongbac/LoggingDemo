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
            if (dal.GetServerLastChangeDate() != null)
            {
                serverLastChangeDate = Convert.ToDateTime(dal.GetServerLastChangeDate().Key);
            }


            result.LastSyncDate = serverLastChangeDate;
            #region post mobile items
            List<MobStockMasterItem> mobItems = new List<MobStockMasterItem>();
            if (model.MobItems != null && model.MobItems.Count > 0)
            {
                foreach (var item in model.MobItems)
                {
                    item.ModifiedOn = DateTime.Now;
                    item.DataState = EDataState.Posted.ToString();
                }
                mobItems  = await dal.CreateMobStockItems(model.MobItems);
            }
            #endregion
            //danh sach item tra ve mob de cap nhat lai
            List<MobStockMasterItem> syncitems = new List<MobStockMasterItem>();
           

            //lay het DB neu la lan dau tien
            if (isFirstSync)
            {
                syncitems = await dal.GetItems(string.Empty, null);
                result.LastSyncDate = DateTime.Now;
                result.ForMobileUpdate = syncitems;
                return result;
            }
            else
            {
                syncitems.AddRange(mobItems);
            }
            #region get change item from server
            //neu kg phai lan dau tien , so sanh voi lan update cuoi
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
            #endregion
            result.ForMobileUpdate = syncitems;

            return result;
        }

    }
}
