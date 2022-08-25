//using Dapper;
//using Dapper.Contrib.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WHMAPI.Models
//{
//    public class MobStockMasterHandler
//    {
//        string ConnectionString;

//        public MobStockMasterHandler(string _connection)
//        {
//            ConnectionString = _connection;
//        }

//        public async Task<List<MobStockMasterItem>> CreateStockItems(List<MobStockMasterItem> items)
//        {
//            long return_item = -1;

//            foreach (var item in items)
//            {
//                //kiem tra va loai bo cac item kg can
//                item.SyncDate = DateTime.Now;
//                item.DataState = "Posted";
//            }
//            List<MobStockMasterItem> toInsert_items = items;

//            using (IDbConnection db = new SqlConnection(ConnectionString))
//            {

//                return_item = await db.InsertAsync<List<MobStockMasterItem>>(items);
//            }
//            if (return_item > 0)
//            {
//                return toInsert_items;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// create item that scan from mobile
//        /// kiem tra xem neu barcode co ton tai thi tu choi
//        /// cac item ma tao boi mobile va chua dua len server thi co the thay doi duoi mob
//        /// 
//        /// </summary>
//        /// <param name="items"></param>
//        /// <returns></returns>
//        public async Task<List<MobStockMasterItem>> CreateMobStockItems(List<MobStockMasterItem> items)
//        {
//            long return_item = -1;
//            List<MobStockMasterItem> toInsert_items = items;
//            IDbConnection db = new SqlConnection(ConnectionString);
//            foreach (var item in toInsert_items)
//            {
//                //kiem tra va loai bo cac item kg can
//                if (db.GetAsync<MobStockMasterItem>(item.ID) != null)
//                {
//                    toInsert_items.Remove(item);
//                }
//                else
//                {
//                    item.SyncDate = DateTime.Now;
//                    item.DataState = "Posted";
//                }
//            }





//            return_item = await db.InsertAsync<List<MobStockMasterItem>>(toInsert_items);

//            if (return_item > 0)
//            {
//                return toInsert_items;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        //string sql = "SELECT * FROM SomeTable WHERE id IN @ids"
//        /// <summary>
//        /// lay tat ca item da modified co ngay > last mob date
//        /// </summary>
//        /// <param name="lastMobChangeDate"></param>
//        /// <returns></returns>
//        public async Task<List<MobStockMasterItem>> GetLastChangeItems(DateTime lastMobChangeDate)
//        {

//            string whereCondition = "ModifiedDate >@LastChangeDate";


//            IDbConnection db = new SqlConnection(ConnectionString);
//            object para = new { LastChangeDate = lastMobChangeDate };
//            return await GetItems(whereCondition, para);
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="whereCondition"></param>
//        /// <param name="para"></param>
//        /// <returns></returns>
//        public async Task<List<MobStockMasterItem>> GetItems(string whereCondition, object para)
//        {
//            string tableName = "G_StockMasterBarCode";

//            string sql = "Select * from " + tableName;
//            if (!string.IsNullOrEmpty(whereCondition))
//            {
//                sql += " WHERE " + whereCondition;
//            }

//            IDbConnection db = new SqlConnection(ConnectionString);

//            var data = await db.QueryAsync<MobStockMasterItem>(sql, para);
//            return data.ToList();
//        }
//        /// <summary>
//        /// 1.Create Items which scaned by Mobile to Server
//        /// 2.Get Items which have modifiedDate>LastsyncDate
//        /// if first sync get all items
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public async Task<SyncMobStockItemResult> MobSyncStockItem(SyncStockItemModel model)
//        {
//            SyncMobStockItemResult result = new SyncMobStockItemResult();
//            bool isFirstSync = model.LastSyncDate == DateTime.MinValue;
//            DateTime serverLastChangeDate = GetServerLastChangeDate();

//            result.LastSyncDate = serverLastChangeDate;

//            List<MobStockMasterItem> to_created = new List<MobStockMasterItem>();
//            if (model.MobItems != null && model.MobItems.Count > 0)
//            {
//                to_created = await CreateMobStockItems(model.MobItems);
//            }


//            List<MobStockMasterItem> syncitems = new List<MobStockMasterItem>();
//            syncitems.AddRange(to_created);

//            if (isFirstSync)
//            {
//                syncitems = await GetItems(string.Empty, null);
//                result.ForMobileUpdate = syncitems;
//                return result;
//            }

//            DateTime mobLastSyncDate = DateTime.MinValue;
//            List<MobStockMasterItem> change_items = new List<MobStockMasterItem>();

//            if (serverLastChangeDate > mobLastSyncDate)
//            {
//                change_items = await GetLastChangeItems(mobLastSyncDate);


//                if (change_items.Count > 0)
//                {
//                    syncitems.AddRange(change_items);
//                }
//            }

//            result.ForMobileUpdate = change_items;

//            return result;
//        }

//        private async Task<List<MobStockMasterItem>> GetAllLastChangeItems(List<MobStockMasterItem> exceptist)
//        {
//            List<MobStockMasterItem> items = await GetItems(string.Empty, null);
//            List<MobStockMasterItem> result = items.Where(p => !exceptist.Any(p2 => p2.ID == p.ID)).ToList();
//            return result;
//        }

//        private DateTime GetServerLastChangeDate()
//        {
//            //Group=[group]='MasterData'
//            //Key=LastChangedDate
//            string tableName = "G_Setting";

//            string sql = "Select * from " + tableName;
//            sql += " WHERE [Group]='MasterData' AND [KEY]='LastChangedDate'";


//            IDbConnection db = new SqlConnection(ConnectionString);

//            SettingUI data = db.QueryFirstOrDefault<SettingUI>(sql);
//            return Convert.ToDateTime(data.Value);
//        }
//    }
//}
