using Dapper;
using Dapper.Contrib.Extensions;
using SCMDAL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SCMDAL.DataHandler
{
    public class MobStockTransHandler
    {
        string tableName = "mssInvTrans";
        string _connectionString;

        public MobStockTransHandler(string _connection)
        {
            _connectionString = _connection;
        }
        /// <summary>
        /// tao 1 giao dich voi dieu kien ID kg ton tai
        /// </summary>
        /// <param name="invTransUI"></param>
        /// <returns></returns>
        public async Task<MobStockTrans> CreateStockTrans(MobStockTrans invTransUI)
        {
           

            string fields = "ID, NUMBER, DESCRIPTION, CREATEDBY, CREATEDON,MODIFIEDON,MODIFIEDBY, USERID, TCODE, StoreNumber,SHELFCODE, TRANSDATE, DATASTATE,SYNCDATE,HID,GLocation";
            string paras = "@ID, @NUMBER,@DESCRIPTION, @CREATEDBY, @CREATEDON,@MODIFIEDON,@MODIFIEDBY, @USERID, @TCODE, @StoreNumber, @SHELFCODE, @TRANSDATE, @DATASTATE,@SYNCDATE,@HID,@GLocation";
            string sql = "INSERT INTO " + tableName + "(" + fields + ") VALUES (" + paras + ")";

            var parameters = new
            {
                ID = invTransUI.ID,
                Number = invTransUI.Number,
                Description = invTransUI.Description,
                CreatedBy = invTransUI.CreatedBy,
                CreatedOn = invTransUI.CreatedOn,
                ModifiedBy = invTransUI.ModifiedBy,
                ModifiedOn = invTransUI.ModifiedOn,
                UserID = invTransUI.UserID,
                TCode = invTransUI.TCode,
                DocNo = invTransUI.Number,
                StoreNumber = invTransUI.StoreNumber,
                ShelfCode = invTransUI.ShelfCode,
                TransDate = invTransUI.TransDate,
                SyncDate = invTransUI.SyncDate,
                DataState = "Posted",
                HID= invTransUI.HID,
                GLOCATION= invTransUI.GLocation
            };
            int result = -1;
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                MobStockTrans existItem = await connection.GetAsync<MobStockTrans>(invTransUI.ID);
                if (existItem != null)
                    return null;
                result = await connection.ExecuteAsync(sql, parameters);
                var resultdetail = await connection.InsertAsync<List<MobStockTransDetail>>(invTransUI.StockTransDetails);
            }


            return invTransUI;
        }
        /// <summary>
        /// luu data vao trong CSDL, update lai cac ID da cap nhat thanh cong
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<List<Guid>> CreateStockTrans(List<MobStockTrans> items)
        {
            List<Guid> return_item = new List<Guid>();

            foreach (var item in items)
            {
                MobStockTrans rec = await CreateStockTrans(item);
                if (rec != null)
                    return_item.Add( rec.ID);
            }
            return return_item;
        }

        public async Task<List<StockTransData>> GetStockTransData(DateTime fromDate, DateTime toDate, string tCode, string storeNo)
        {
            List<StockTransData> data = new List<StockTransData>();
            var procedure = "[InvTransByDate]";
            var values = new { Beginning_Date = fromDate, Ending_Date = toDate, TCode = tCode, StoreNo = storeNo };
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var results = await connection.QueryAsync<StockTransData>(procedure, values, commandType: CommandType.StoredProcedure);
                    data = results.ToList();
                }
                catch (Exception)
                {

                }
            }
            return data;
        }


        public async Task<List<MobStockTrans>> GetStockTrans(DateTime fromDate, DateTime toDate, string storeNumber)
        {
            List<MobStockTrans> data = new List<MobStockTrans>();
           
            var paras = new { FromDate = fromDate, ToDate = toDate,StoreNumber = storeNumber };
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    string sql="Select * from "+tableName +" WHERE TransDate>=@FromDate AND TransDate<=@ToDate AND StoreNumber=@StoreNumber";
                    string sqldetail = "Select * from  mssInvTransDetail WHERE TransID=@TransID";
                    var results = await connection.QueryAsync<MobStockTrans>(sql, paras);
                    
                    if (results==null || results.Count()==0)
                    {
                        return null;
                    }

                    //else
                    data = results.ToList();
                    foreach (var item in data)
                    {
                        Guid transID = item.ID;
                        var detailresults = await connection.QueryAsync<MobStockTransDetail>
                            (sqldetail, new {TransID= transID });
                        List<MobStockTransDetail> details = detailresults.ToList();

                        item.StockTransDetails.AddRange(detailresults.ToList());
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return data;
        }

    }
}
