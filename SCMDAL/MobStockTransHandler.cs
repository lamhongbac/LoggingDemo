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
        string tableName = "StockTrans";
        string ConnectionString;

        public MobStockTransHandler(string _connection)
        {
            ConnectionString = _connection;
        }
        public async Task<int> CreateStockTrans(MobStockTrans invTransUI)
        {
            string fields = "ID, NUMBER, DESCRIPTION, CREATEDBY, CREATEDON,MODIFIEDON,MODIFIEDBY, USERID, TCODE, StoreNumber,SHELFCODE, TRANSDATE, DATASTATE,SYNCDATE";
            string paras = "@ID, @NUMBER,@DESCRIPTION, @CREATEDBY, @CREATEDON,@MODIFIEDON,@MODIFIEDBY, @USERID, @TCODE, @StoreNumber, @SHELFCODE, @TRANSDATE, @DATASTATE,@SYNCDATE)";
            string sql = "INSERT INTO " + tableName + "(" + fields + ")VALUES (" + paras + ")";

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
                DataState = "Posted"
            };
            int result = -1;
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                result = await connection.ExecuteAsync(sql, parameters);
                var resultdetail = await connection.InsertAsync<List<StockTransDetail>>(invTransUI.StockTransDetails);
            }


            return result;
        }
        public async Task<int> CreateStockTrans(List<MobStockTrans> items)
        {
            int return_item = 0;

            foreach (var item in items)
            {
                return_item+= await CreateStockTrans(item);
            }
            return return_item;
        }
    }
}
