using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MSAMobApp.Services
{
  public  class ApiServices
    {
        //https://localhost:44346/api/StockMasterItem/CreateStockItems
        private static  string baseURL = "https://localhost:44346/api/";
        private const string getStockMasterUrl = "";
        private const string updateStockMasterUrl = "";
        private const string createStockMasterUrl = "StockMasterItem/CreateStockItems";

        public static string BaseURL => baseURL;
        public static string GetStockMasterUrl  => getStockMasterUrl; 
        public static string UpdateStockMasterUrl => updateStockMasterUrl;
        public static string CreateStockMasterUrl => createStockMasterUrl;               
    }
}
