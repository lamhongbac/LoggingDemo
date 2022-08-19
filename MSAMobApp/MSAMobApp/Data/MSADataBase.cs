using MSAMobApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MSAMobApp.Data
{
    public class MSADataBase
    {
        private static string dbName = "MSAMobDB.db";
        static SQLiteAsyncConnection database;
        public async static Task Init()
        {
            if (database != null)
            {
               
                return;
            }
            var databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<MobStockMasterItem>().Wait();
            database.CreateTableAsync<StockTrans>().Wait();
            database.CreateTableAsync<StockTransDetail>().Wait();
            database.CreateTableAsync<AppSetting>().Wait();
            //database.CreateTableAsync<AppSetting>().Wait();
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

        

        /// <summary>
        /// quet barcode and add to DB
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userID"></param>
        /// <param name="barcode"></param>
        //public async static Task AddStock(StockTrans stock)
        //{
        //await Init();
        //await database.InsertAsync(stock);
        //var stockSample = await GetMasterStockItemAsync(stock.BarCode);

        //if (stockSample == null)
        //{
        //    MobStockMasterItem newStockSample = new
        //         MobStockMasterItem()
        //    {
        //        BarCode = stock.BarCode,
        //        CreatedBy = "Demo",
        //        CreatedOn = DateTime.Now,
        //        DataState = EDataState.New.ToString(),
        //        ID = Guid.NewGuid(),
        //        Name = string.Empty,
        //        Unit = string.Empty,
        //        ModifiedBy = "Demo",
        //        ModifiedDate = DateTime.Now
        //    };

        //    await AddStockSample(newStockSample);

        //}
        //Console.WriteLine("{0} == {1}", stock.BarCode, stock.ID);
        //}
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
                item.DataState = EDataState.Posted.ToString();
                var existitem=await database.GetAsync<MobStockMasterItem>(item.ID);

                if (existitem != null)
                {
                    rows =+ await database.UpdateAsync(item);
                    
                }
                else
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
    }
}
