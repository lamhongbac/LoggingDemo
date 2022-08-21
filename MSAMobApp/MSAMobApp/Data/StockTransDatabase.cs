using MSAMobApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSAMobApp.Data
{
  public  class StockTransDatabase
    {
         SQLiteAsyncConnection _database;
        public StockTransDatabase(SQLiteAsyncConnection database)
        {
            _database = database;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockTrans"></param>
        public async  Task CreateStockTrans(StockTrans stockTrans)
        {
            try
            {
                //Save to Local with Status= New
                await _database.InsertAsync(stockTrans);
                //Read local and Save to DB, Update status= Posted
                foreach (var item in stockTrans.StockTransDetails)
                {
                    await _database.InsertAsync(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
        public async  Task<List<StockTrans>> GetStockTrans()
        {
           return await _database.Table<StockTrans>().ToListAsync();
        }
    }
}
