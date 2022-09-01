using MSAMobApp.DataBase;
using SCMDAL.DTO;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MSAMobApp.Services
{
    /// <summary>
    /// stock transaction service , deal with API/DB
    /// </summary>
    public static class StockTransDBService
    {



        /// <summary>
        /// get stock transaction from DB
        /// para=GetStockTransModel
        /// </summary>
        /// <returns></returns>
        public static async Task<List<StockTrans>> GetStockTrans(object paraModel)
        {
            HttpClientHelper<List<StockTrans>> stockTransService = new HttpClientHelper<List<StockTrans>>(ApiServices.BaseURL);
            string apiURL = ApiServices.GetStockTransUrl;
            return await stockTransService.PostRequest(apiURL, paraModel, new CancellationToken(false));
        }
        /// <summary>
        /// Create stock trans on DB by using API
        /// </summary>
        /// <param name="paraModel">StockTrans</param>
        /// <returns></returns>
        public static async Task<bool> CreateStockTrans(StockTrans paraModel)
        {
            MobStockTrans dbStockTrans = ConvertToDBStockTrans(paraModel);
            bool OK = false;
            List<MobStockTrans> stock_trans = new List<MobStockTrans>() { dbStockTrans };

            HttpClientHelper<bool> stockTransService = new HttpClientHelper<bool>(ApiServices.BaseURL);
            string apiURL = ApiServices.CreateStockTransUrl;
            try
            {
                OK = await stockTransService.PostRequest(apiURL, stock_trans, new CancellationToken(false));
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                OK = false;
                return OK;
            }
            return OK;
        }

        private static MobStockTrans ConvertToDBStockTrans(StockTrans paraModel)
        {
            MobStockTrans db = new MobStockTrans();
            db.CreatedBy = paraModel.CreatedBy;
            db.CreatedOn = paraModel.CreatedOn;
            db.DataState = paraModel.DataState;
            db.Description = paraModel.Description;
            db.GLocation = paraModel.GLocation;
            db.HID = paraModel.HID;
            db.ID= paraModel.ID;
            db.ModifiedBy= paraModel.ModifiedBy;
            db.ModifiedOn = paraModel.ModifiedOn;

            db.Number= paraModel.Number;
            db.ShelfCode = paraModel.ShelfCode;
            db.StoreNumber = paraModel.CreatedBy;
            db.SyncDate = paraModel.SyncDate;
            db.TCode = paraModel.TCode;
            db.TransDate = paraModel.TransDate;
            db.UserID = paraModel.UserID;
            db.StockTransDetails = ConvertStockTransDetails(paraModel.StockTransDetails);
            return db;

        }

        private static List<MobStockTransDetail> ConvertStockTransDetails(List<StockTransDetail> stockTransDetails)
        {
            List<MobStockTransDetail> ret = new List<MobStockTransDetail>();
            foreach (var item in stockTransDetails)
            {
                MobStockTransDetail retitem = new MobStockTransDetail()
                {
                    BarCode = item.BarCode,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = item.CreatedOn,
                    DataState = item.DataState,
                    ID = item.ID,
                    ItemNumber = item.ItemNumber,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedOn = item.ModifiedOn,
                    Quantity = item.Quantity,
                    ScanDateTimes = item.ScanDateTimes,
                    TransID = item.TransID


                };
                ret.Add(retitem);
            }
            return ret;
        }
    }
}
