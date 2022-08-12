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
        static SQLiteAsyncConnection database;
        public async static Task Init()
        {
            if (database != null)
                return;
            var databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "MyData.db");
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<StockTrans>().Wait();
            database.CreateTableAsync<MobStockMasterItem>().Wait();
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
        public async static Task<int> AddStockSample(MobStockMasterItem stockSample)
        {
            await Init();
            stockSample.CreatedOn = DateTime.Now;
            stockSample.ModifiedOn = DateTime.Now;           
            // await database.InsertAsync(stock);
           return await database.InsertAsync(stockSample);

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
        public async static Task<int> UpdateAsyncStockSample(MobStockMasterItem updated_item)
        {
            await Init();
            MobStockMasterItem item = await GetMasterStockItemAsync(updated_item.ID);

            if (item != null)
            {
                item.ModifiedOn = DateTime.Now;
                if (item.DataState == EDataState.Posted.ToString())
                    item.DataState = EDataState.Edited.ToString(); 

                if (item.DataState== EDataState.New.ToString())
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
