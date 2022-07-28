using MSAMobApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSAMobApp.Data
{
    public class MSADataBase
    {
        readonly SQLiteAsyncConnection database;
        public MSADataBase(string dbPath)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<StockTrans>().Wait();
        }
        /// <summary>
        /// quet barcode and add to DB
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userID"></param>
        /// <param name="barcode"></param>
        public static void AddStock(SQLiteConnection db, string userID, string barcode)
        {
            var stock = new StockTrans()
            {
                BarCode = barcode,
                DataStatus = "New",
                Direction = "In",
                ID = Guid.NewGuid(),
                Quantity = 1,
                ScanDateTimes = DateTime.Now,
                SelfCode = "SDemo",
                UserID = userID,
                WHCode = "WDemo"


            };
            db.Insert(stock);
            Console.WriteLine("{0} == {1}", stock.BarCode, stock.ID);
        }
    }
}
