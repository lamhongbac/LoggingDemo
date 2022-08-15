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
        private const string createStockMasterUrl = "/api/StockMasterItem/CreateStockItems";
        
        public static string BaseURL => baseURL;
        public static string GetStockMasterUrl  => getStockMasterUrl; 
        public static string UpdateStockMasterUrl => updateStockMasterUrl;
        public static string CreateStockMasterUrl => createStockMasterUrl;
        //SyncStockItems
        public static string SyncStockItems => "SyncStockItems";

    }
}
