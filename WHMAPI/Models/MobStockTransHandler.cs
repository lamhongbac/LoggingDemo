//using Dapper;
//using Dapper.Contrib.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WHMAPI.Models
//{
//    public class MobStockTransHandler
//    {
//        string tableName = "StockTrans";
//        string ConnectionString;

//        public MobStockTransHandler(string _connection)
//        {
//            ConnectionString = _connection;
//        }
//        public async Task<int> CreateStockTrans(MobStockTrans invTransUI)
//        {
//            string fields = "ID, NUMBER, NAME, DESCRIPTION, CREATEDBY, CREATEDON, USERID, TCODE, DOCNO, WHCODE,SHELFCODE, TRANSDATE, DATASTATE";
//            string paras = "@ID, @NUMBER, @NAME, @DESCRIPTION, @CREATEDBY, @CREATEDON, @USERID, @TCODE, @DOCNO, @WHCODE, @SHELFCODE, @TRANSDATE, @DATASTATE)";
//            string sql = "INSERT INTO " + tableName + "(" + fields + ")VALUES (" + paras + ")";

//            var parameters = new
//            {
//                ID = invTransUI.ID,
//                Number = invTransUI.Number,
//                Description = invTransUI.Description,
//                CreatedBy = invTransUI.CreatedBy,
//                CreatedOn = invTransUI.CreatedOn,
//                UserID = invTransUI.UserID,
//                TCode = invTransUI.TCode,
//                DocNo = invTransUI.Number,
//                StoreNumber = invTransUI.StoreNumber,
//                ShelfCode = invTransUI.ShelfCode,
//                TransDate = invTransUI.TransDate,
//                SyncDate = invTransUI.SyncDate,
//                DataState = "Posted"
//            };
//            int result = -1;
//            using (IDbConnection connection = new SqlConnection(ConnectionString))
//            {
//                result = await connection.ExecuteAsync(sql, parameters);
//                var resultdetail = await connection.InsertAsync<List<StockTransDetail>>(invTransUI.StockTransDetails);
//            }


//            return result;
//        }
//        public async Task<List<MobStockTrans>> CreateStockTrans(List<MobStockTrans> items)
//        {
//            long return_item = -1;

//            foreach (var item in items)
//            {
//                //kiem tra va loai bo cac item kg can
//                item.SyncDate = DateTime.Now;
//                item.DataState = "Posted";
//            }
//            List<MobStockTrans> toInsert_items = items;

//            using (IDbConnection db = new SqlConnection(ConnectionString))
//            {

//                return_item = await db.InsertAsync<List<MobStockTrans>>(items);
//            }
//            if (return_item > 0)
//            {
//                return toInsert_items;
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
