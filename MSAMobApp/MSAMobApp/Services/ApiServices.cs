using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MSAMobApp.Services
{
  public  class ApiServices
    {
        //http://localhost:31446/WeatherForecast
        //http://localhost:31446
        //https://localhost:44346/api/StockMasterItem/CreateStockItems
        private static  string baseURL = $"http://10.0.2.2:31446";
        private const string getStockMasterUrl = "";
        private const string updateStockMasterUrl = "";
        private const string createStockMasterUrl = "/api/MasterData/CreateStockItems";
        ///api/MasterData/SyncStockItems
        private const string syncStockMasterUrl = "/api/MasterData/SyncStockItems";
        private const string getStockTransUrl = "/api/StockTrans/GetMobStockTrans";
        private const string createStockTransUrl = "/api/StockTrans/CreateMobStockTrans";

        public static string BaseURL => baseURL;
        public static string GetStockMasterUrl  => getStockMasterUrl;
        public static string GetStockTransUrl => getStockTransUrl;
        public static string CreateStockTransUrl => createStockTransUrl;

        public static string UpdateStockMasterUrl => updateStockMasterUrl;
        public static string CreateStockMasterUrl => createStockMasterUrl;
        public static string SyncStockMasterUrl => syncStockMasterUrl;
        //SyncStockItems
        //public static string SyncStockItems => "SyncStockItems";

    }
}
