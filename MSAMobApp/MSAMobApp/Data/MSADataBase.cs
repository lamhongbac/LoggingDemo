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
                DataState = "New",
                Direction = "In",
                ID = Guid.NewGuid(),
                Quantity = 1,
                ScanDateTimes = DateTime.Now,
                SelfCode = "SDemo",
                UserID = userID,
                WHCode = "WDemo"


            };
            await database.InsertAsync(stock);
            Console.WriteLine("{0} == {1}", stock.BarCode, stock.ID);
        }



        public static void AddStockSample(SQLiteConnection db, string userID, string barcode)
        {
            var stock = new StockSample()
            {
                BarCode = barcode,
                DataState = "New",
                CreatedBy = userID,
                CreatedDate = DateTime.Now,
                Name = string.Empty,
                Unit = "Unit",
                ID = Guid.NewGuid(),
                ModifiedBy = userID,
                ModifiedDate = DateTime.Now

            };
            db.Insert(stock);
            
        }
    }
}
