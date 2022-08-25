using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WHMAPI.Models
{
    public class MobStockTransHandler
    {
        string ConnectionString;

        public MobStockTransHandler(string _connection)
        {
            ConnectionString = _connection;
        }

        public async Task<List<MobStockTrans>> CreateStockTrans(List<MobStockTrans> items)
        {
            long return_item = -1;

            foreach (var item in items)
            {
                //kiem tra va loai bo cac item kg can
                item.SyncDate = DateTime.Now;
                item.DataState = "Posted";
            }
            List<MobStockTrans> toInsert_items = items;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {

                return_item = await db.InsertAsync<List<MobStockTrans>>(items);
            }
            if (return_item > 0)
            {
                return toInsert_items;
            }
            else
            {
                return null;
            }
        }
    }
}
