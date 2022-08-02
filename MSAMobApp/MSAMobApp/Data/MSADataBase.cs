﻿using MSAMobApp.Models;
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
            database.CreateTableAsync<StockSample>().Wait();
        }
        /// <summary>
        /// quet barcode and add to DB
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userID"></param>
        /// <param name="barcode"></param>
        public async static Task AddStock(string userID, string barcode)
        {
            await Init();
            var stock = new StockTrans()
            {
                BarCode = barcode,
                DataState = EDataState.New.ToString(),
                Direction = "In",
                ID = Guid.NewGuid(),
                Quantity = 1,
                ScanDateTimes = DateTime.Now,
                SelfCode = "SDemo",
                UserID = userID,
                WHCode = "WDemo"


            };
            await database.InsertAsync(stock);
            //Console.WriteLine("{0} == {1}", stock.BarCode, stock.ID);
        }
        #region stockSample
        /// <summary>
        /// Get One Item stockSample
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async static Task<StockSample> GetStockISampletemAsync(Guid itemId)
        {
            await Init();
            var stockSample = await database.Table<StockSample>().Where(x => x.ID == itemId).FirstOrDefaultAsync();
            return stockSample;
        }


        /// <summary>
        /// Add one Item stockSample by scaning barcode, add name and unit
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public async static Task AddStockSample(StockSample stockSample)
        {
            await Init();
            stockSample.CreatedDate = DateTime.Now;
            stockSample.ModifiedDate = DateTime.Now;           
            // await database.InsertAsync(stock);
            await database.InsertAsync(stockSample);

        }
        /// <summary>
        /// Update stock sample
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="itemID"></param>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public async static Task<int> UpdateAsyncStockSample(string userID,Guid itemID,
            string name, string unit)
        {
            StockSample item = await GetStockISampletemAsync(itemID);
            item.Name = name; 
            item.Unit = unit;
            item.ModifiedBy = userID;
            item.ModifiedDate = DateTime.Now;
            if (item.DataState == EDataState.Posted.ToString())
                item.DataState = EDataState.Edited.ToString(); ;

            return await database.UpdateAsync(item);
        }
        public static async Task<IEnumerable<StockSample>> GetStockSamples()
        {
            await Init();
            var stockSamples = await database.Table<StockSample>().ToListAsync();
            return stockSamples;
        }
        #endregion
    }
}
