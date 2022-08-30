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
        public async  Task<bool> CreateStockTrans(StockTrans stockTrans)
        {
            int rec = -1;
            try
            {
                var find = _database.Table<StockTrans>().Where(x => x.ID == stockTrans.ID).FirstOrDefaultAsync();
                //var appSetting = await database.Table<AppSetting>().Where(x => x.AppGroup == group && x.AppKey == key).FirstOrDefaultAsync();
                if (find != null)
                    return false;

                //Save to Local with Status= New
                rec =   await _database.InsertAsync(stockTrans);
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

            return rec > 0;
        }
        public async  Task<List<StockTrans>> GetStockTrans()
        {
           return await _database.Table<StockTrans>().ToListAsync();
        }
    }
}
