using MSAMobApp.Data;
using MSAMobApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSAMobApp.Services
{
    /// <summary>
    /// Tap trung vao cac operation lien quan den sync data with server
    /// </summary>
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
        /// lay danh sach moi tao (New)-> update len server
        /// Kiem tra danh sach server moi tao >lastUpdate
        /// lay ve danh sach moi tao luu vao mobile 
        /// Update lai ngay lastUpdate= server last change
        /// </summary>
        /// <param name="paraModel"></param>
        /// <returns></returns>
        public async Task<SyncMobStockItemResult> SyncStockItems()
        {
            List<MobStockMasterItem> toCreateItems = await MSADataBase.GetNewMasterStockItemAsync();
            AppSetting LastSyncData = await MSADataBase.GetLastSyncData("Sync","LastSyncDate");
            DateTime LastSyncDate = DateTime.MinValue;
            if (LastSyncData != null)
            {
                 LastSyncDate = Convert.ToDateTime(LastSyncData.AppValue);
            }
            
            SyncStockItemModel postObject = new SyncStockItemModel()
            {
                LastSyncDate = LastSyncDate,
                 MobItems= toCreateItems
            };


            string apiURL = ApiServices.CreateStockMasterUrl;
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(postObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            string apiUrl = ApiServices.BaseURL + apiURL;

            var response = await client.PostAsync(apiUrl, data);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                SyncMobStockItemResult result = JsonConvert.DeserializeObject<SyncMobStockItemResult>(content);
                //update sync result to local
                await MSADataBase.UpdateSyncAsyncStockMasterItems(result.ForMobileUpdate);
                await MSADataBase.UpdateSyncDate(result.LastSyncDate);

                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            
            //var result= await httpClientHelper.PostRequest(apiURL, paraModel, new CancellationToken(false));
            //return result;
        }
    }
}
