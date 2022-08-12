using MSAMobApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSAMobApp.Services
{
   public class StockMasterService
    {

        /// <summary>
        /// lay ve 1 danh sach item theo dieu kien
        /// </summary>
        /// <param name="paraModel"></param>
        /// <returns></returns>
        public async Task<List<MobStockMasterItem>> GetStockItems(object paraModel)
        {
            HttpClientHelper<List<MobStockMasterItem>> httpClientHelper =
                new HttpClientHelper<List<MobStockMasterItem>>(ApiServices.BaseURL);
            string apiURL = ApiServices.GetStockMasterUrl;
            return await httpClientHelper.PostRequest(apiURL, paraModel, new CancellationToken(false));
        }


        /// <summary>
        /// update 1 danh items
        /// </summary>
        /// <param name="paraModel"></param>
        /// <returns></returns>
        public async Task<int> UpdateStockItems(object paraModel)
        {
            HttpClientHelper<int> httpClientHelper =
                new HttpClientHelper<int>(ApiServices.BaseURL);
            string apiURL = ApiServices.UpdateStockMasterUrl;
            return await httpClientHelper.PostRequest(apiURL, paraModel, new CancellationToken(false));
        }


        /// <summary>
        /// tao 1 danh sach stock Items
        /// </summary>
        /// <param name="paraModel"></param>
        /// <returns></returns>
        public async Task<int> CreateStockItems(object paraModel)
        {
            HttpClientHelper<int> httpClientHelper =
                new HttpClientHelper<int>(ApiServices.BaseURL);
            string apiURL = ApiServices.UpdateStockMasterUrl;
            return await httpClientHelper.PostRequest(apiURL, paraModel, new CancellationToken(false));
        }
    }
}
