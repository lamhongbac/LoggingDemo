using MSAMobApp.DataBase;
using MSAMobApp.Models;
using MSAMobApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MSAMobApp.Data
{
    public class MSADataBase
    {
        private static string dbName = "MSAMobDB.db";
        static SQLiteAsyncConnection database;
        static StockTransDatabase stockTransLocalDBHandler;
        public async static Task Init()
        {
            if (database != null)
            {
                //if (stockTransHandler==null)
                //    stockTransHandler = new StockTransDatabase(database);
                return;
            }
            var databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<MobStockMasterItem>().Wait();
            database.CreateTableAsync<StockTrans>().Wait();
            database.CreateTableAsync<StockTransDetail>().Wait();
            database.CreateTableAsync<AppSetting>().Wait();
            //database.CreateTableAsync<AppSetting>().Wait();
            stockTransLocalDBHandler = new StockTransDatabase(database);
        }
        /// <summary>
        /// Xoa het cac bang de tao cau truc bang moi
        /// </summary>
        public static bool DropAllTables()
        {
            bool result = false;
            try
            {
                var databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);
                SQLiteAsyncConnection database = new SQLiteAsyncConnection(databasePath);

                string stocktransDetailTable = "StockTransDetail";
                string stockTransTable = "StockTrans";
                string mobStockMasterItemTable = "MobStockMasterItem";

                string sqlexist_stocktransDetailTable = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name ='" + stocktransDetailTable + "'";
                string sqlexist_stockTransTable = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name ='" + stockTransTable + "'";
                string sqlexist_mobStockMasterItemTable = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name ='" + mobStockMasterItemTable + "'";

                //kiem tra neu exist thi xoa

                database.DropTableAsync<StockTransDetail>();
                database.DropTableAsync<StockTrans>();
                database.DropTableAsync<MobStockMasterItem>();
                result = true;
            }
            catch(Exception ex)
            {
                
                result = false;
            }
            return result;
        }

        public async static Task<AppSetting> GetLastSyncData(string group, string key)
        {
            await Init();
            var appSetting = await database.Table<AppSetting>().Where(x => x.AppGroup == group && x.AppKey == key).FirstOrDefaultAsync();
            return appSetting;
        }

        public async static Task UpdateSyncDate(DateTime lastSyncDate)
        {
            await Init();
            AppSetting appSetting = await GetLastSyncData("Sync", "LastSyncDate");
            if (appSetting == null)
            {
                await database.InsertAsync(new AppSetting() 
                { 
                    AppGroup= "Sync", 
                    AppKey= "LastSyncDate",
                    AppValue= lastSyncDate.ToString() 
                }); 

            }
            else
            {
                appSetting.AppValue = lastSyncDate.ToString();
                await database.UpdateAsync(appSetting);
            }

        }

        

  
        #region stockSample
        /// <summary>
        /// Get One Item stockSample
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async static Task<MobStockMasterItem> GetMasterStockItemAsync(Guid itemId)
        {
            await Init();
            var stockSample = await database.Table<MobStockMasterItem>().Where(x => x.ID == itemId).FirstOrDefaultAsync();
            return stockSample;
        }
        public async static Task<List<MobStockMasterItem>> GetNewMasterStockItemAsync()
        {
            await Init();
            string dataState = EDataState.New.ToString();
            var stockSample = await database.Table<MobStockMasterItem>().Where(x => x.DataState == dataState).ToListAsync();
            return stockSample;
        }
        public async static Task<MobStockMasterItem> GetMasterStockItemAsync(string barcode)
        {
            await Init();
            var stockSample = await database.Table<MobStockMasterItem>().Where(x => x.BarCode == barcode).FirstOrDefaultAsync();
            return stockSample;
        }
        /// <summary>
        /// Add one Item stockSample by scaning barcode, add name and unit
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public async static Task<int> AddStockSample(MobStockMasterItem masterStockItem)
        {
            await Init();
            masterStockItem.CreatedOn = DateTime.Now;
            masterStockItem.ModifiedOn = DateTime.Now;           
            // await database.InsertAsync(stock);
           return await database.InsertAsync(masterStockItem);

        }
        /// <summary>
        /// Update Sync List Items return from Data center Server.
        /// Cac field SyncDate, dataState dc tra ve tu server
        /// data state=Posted , co nghia la item nay update tu server
        /// </summary>
        /// <param name="updated_items"></param>
        /// <returns></returns>
        public async static Task<int> UpdateSyncAsyncStockMasterItems(List<MobStockMasterItem> synced_items)
        {
            await Init();
            int rows = 0;
            foreach (var item in synced_items)
            {
                //item.DataState = EDataState.Posted.ToString();
                //var existitem=await database.GetAsync<MobStockMasterItem>(item.ID);
                MobStockMasterItem existitem =await database.Table<MobStockMasterItem>().
                    FirstOrDefaultAsync(p => p.BarCode== item.BarCode);
                
                if (existitem != null) //tim thay
                {
                    existitem.CreatedBy = item.CreatedBy;
                    existitem.CreatedOn = item.CreatedOn;
                    existitem.DataState = item.DataState;
                    existitem.Description = item.Description;
                    existitem.ModifiedBy = item.ModifiedBy;
                    existitem.ModifiedOn = item.ModifiedOn;
                    existitem.Name = item.Name;
                    existitem.Number = item.Number;
                    existitem.SyncDate = item.SyncDate;
                    existitem.UserID = item.UserID;
                    existitem.Unit = item.Unit;
                    existitem.GLocation = item.GLocation;
                    existitem.HID = item.HID;

                    rows =+ await database.UpdateAsync(item);                    
                }
                else //khong tim thay
                {
                    rows += await database.InsertAsync(item);
                }
            }
            return rows;
        }

        private static async Task<int> CreateServerStockMaster(MobStockMasterItem item)
        {
            return await database.InsertAsync(item);
        }

        /// <summary>
        /// Update sync stock item from server
        /// Cac field SyncDate, dataState dc tra ve tu server
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="itemID"></param>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        private async static Task<int> UpdateServerStockMaster(MobStockMasterItem synced_item)
        {
            await Init();
            return await database.UpdateAsync(synced_item);
        }
        public async static Task<int> UpdateAsyncStockMasterItems(List<MobStockMasterItem> updated_items)
        {
            int rows = -1;
            foreach (var item in updated_items)
            {
                item.DataState = EDataState.Posted.ToString();
                   rows+= await  UpdateAsyncStockMaster(item);
            }
            if (rows > 0)
                return rows + 1;
            else
                return rows;
        }
        /// <summary>
        /// Update stock item
        /// Unit/Name only
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="itemID"></param>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public async static Task<int> UpdateAsyncStockMaster(MobStockMasterItem updated_item)
        {
            await Init();
            MobStockMasterItem item = await GetMasterStockItemAsync(updated_item.ID);

            if (item != null)
            {
                item.ModifiedOn = DateTime.Now;
                if (item.DataState == EDataState.Posted.ToString())
                    item.DataState = EDataState.Edited.ToString();

                if (item.DataState == EDataState.New.ToString())
                    //do notthing;

                    item.Name = updated_item.Name;
                item.Unit = updated_item.Unit;

                return await database.UpdateAsync(item);
            }
            else
            {
                return -1;
            }
        }
        public static async Task<IEnumerable<MobStockMasterItem>> GetStockMasterItems()
        {
            await Init();
            var stockSamples = await database.Table<MobStockMasterItem>().ToListAsync();
            return stockSamples;
        }
        #endregion

        #region stock Transactions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockTrans"></param>
        /// <returns></returns>
        public async static Task<bool> CreateStockTrans(StockTrans stockTrans)
        {
            bool OK = await stockTransLocalDBHandler.CreateStockTrans(stockTrans);
            if (OK)
            {
                List<StockTrans> stock_trans = new List<StockTrans>() { stockTrans };
                bool server_updated = await StockTransDBService.CreateStockTrans(stock_trans);
                if (server_updated)
                {
                    stockTrans.DataState = EDataState.Posted.ToString();
                    stockTrans.SyncDate = DateTime.Now;
                    await stockTransLocalDBHandler.UpdateStockTrans(stockTrans);
                }
            }
            return OK;
        }
        /// <summary>
        /// Ham trung gian
        /// </summary>
        /// <returns></returns>
        public static async Task<List<StockTrans>> GetStockTrans()
        {
            stockTransLocalDBHandler = new StockTransDatabase(database);
          List<StockTrans> data=await  stockTransLocalDBHandler.GetStockTrans();
            return data;
                //await database.Table<StockTrans>().ToListAsync();


        }
        #endregion
    }
}
