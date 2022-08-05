using MSAMobApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MSAMobApp.Services
{
  public static  class StockTransService
    {
      static  SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db!=null)
            {
                return;
            }

            // SQLiteAsyncConnection database;
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MobData.db");
            db = new SQLiteAsyncConnection(dbPath);
          await  db.CreateTableAsync<StockTrans>();

        }
        public static async Task AddStock(SQLiteConnection db, string userID, string barcode)
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
                ShelfCode = "SDemo",
                UserID = userID,
                WHCode = "WDemo"


            };
            db.Insert(stock);
            Console.WriteLine("{0} == {1}", stock.BarCode, stock.ID);
        }
    }
}
