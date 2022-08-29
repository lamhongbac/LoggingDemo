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
    public class MobStockMasterHandler
    {
        string ConnectionString;

        public MobStockMasterHandler(string _connection)
        {
            ConnectionString = _connection;
        }

        //public async Task<List<MobStockMasterItem>> CreateMobStockItems(List<MobStockMasterItem> items)
        //{
        //    long return_item = -1;

        //    List<MobStockMasterItem> toInsert_items = items;
        //    using (IDbConnection db = new SqlConnection(ConnectionString))
        //    {
        //        return_item = await db.InsertAsync<List<MobStockMasterItem>>(items);
        //    }
        //    if (return_item > 0)
        //    {
        //        return toInsert_items;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Chi danh cho viec create item from Mobile
        /// Create item that scan from mobile
        /// kiem tra xem neu barcode co ton tai thi tu choi, quay ve se xoa khoi Local
        /// cac item ma tao boi mobile va chua dua len server thi co the thay doi duoi mob
        /// 
        /// CHU Y: kg cap nhat lastChangeDate
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<List<MobStockMasterItem>> CreateMobStockItems(List<MobStockMasterItem> items)
        {

            List<MobStockMasterItem> toInsert_items = items;
            IDbConnection db = new SqlConnection(ConnectionString);
            foreach (var item in toInsert_items)
            {
                //kiem tra va loai bo cac item kg can
                if (db.GetAsync<MobStockMasterItem>(item.ID) != null)
                {
                    toInsert_items.Remove(item);
                }

            }

            int return_item = await db.InsertAsync<List<MobStockMasterItem>>(toInsert_items);

            return toInsert_items;

        }

        //string sql = "SELECT * FROM SomeTable WHERE id IN @ids"
        /// <summary>
        /// lay tat ca item da modified co ngay > last mob date
        /// </summary>
        /// <param name="lastMobChangeDate"></param>
        /// <returns></returns>
        public async Task<List<MobStockMasterItem>> GetLastChangeItems(DateTime lastMobChangeDate)
        {

            string whereCondition = "ModifiedDate >@LastChangeDate";


            IDbConnection db = new SqlConnection(ConnectionString);
            object para = new { LastChangeDate = lastMobChangeDate };
            return await GetItems(whereCondition, para);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<List<MobStockMasterItem>> GetItems(string whereCondition, object para)
        {
            string tableName = "G_StockMasterBarCode";

            string sql = "Select * from " + tableName;
            if (!string.IsNullOrEmpty(whereCondition))
            {
                sql += " WHERE " + whereCondition;
            }

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {

                var data = await db.QueryAsync<MobStockMasterItem>(sql, para);
                return data.ToList();
            }
            //return new List<MobStockMasterItem>();
            
        }
        
        private async Task<List<MobStockMasterItem>> GetAllLastChangeItems(List<MobStockMasterItem> exceptist)
        {
            List<MobStockMasterItem> items = await GetItems(string.Empty, null);
            List<MobStockMasterItem> result = items.Where(p => !exceptist.Any(p2 => p2.ID == p.ID)).ToList();
            return result;
        }

        /// <summary>
        /// chu y ModifiedDate, va cac thuoc tinh khac can su chuan bi tu ben ngoai
        /// b1. loai bo cac item dang co trong CSDL ra khoi danh sach
        /// b2. thuc hien trong 1 transaction
        /// b3. tra ve so dong da import, hoac kg import dc gi ca (-1)
        /// </summary>
        /// <param name="inmport_data"></param>
        /// <returns></returns>
        public async Task<int> ImportData(List<MobStockMasterItem> inmport_data)
        {
            int record = -1;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    string findExistStockSql = @"SELECT * FROM G_StockMasterBarCode where BarCode In @BarCodeList";
                    List<string> barcodes = inmport_data.Select(x => x.BarCode).ToList();
                    var parameters = new DynamicParameters();
                    parameters.Add("@BarCodeList", barcodes);

                    var existitems = await connection.QueryAsync<MobStockMasterItem>(findExistStockSql, parameters);

                    List<MobStockMasterItem> existStockitems = existitems.ToList();
                    List<MobStockMasterItem> toImportItem = new List<MobStockMasterItem>();

                    if (existStockitems != null && existStockitems.Count > 0)
                        //toImportItem = inmport_data.Where(p => !existStockitems.Any(p2 => p2.ID == p.ID));
                        toImportItem = inmport_data.Where(p => existStockitems.All(p2 => p2.BarCode != p.BarCode)).ToList();
                    else
                        toImportItem = inmport_data;
                    if (toImportItem.Count > 0)
                    {
                        string fields = "ID,BarCode,Number,Name,Unit,Description,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,DataState,HID,UserID,GLocation,SyncDate";
                        string paras = "@ID,@BarCode,@Number,@Name,@Unit,@Description,@CreatedOn,@CreatedBy,@ModifiedOn,@ModifiedBy,@DataState,@HID,@UserID,@GLocation,@SyncDate";

                        string insert_sql = "INSERT INTO G_StockMasterBarCode(" + fields + ")VALUES (" + paras + ")";
                        //if (connection==null)                    
                        //      connection = new SqlConnection(_connectionString);
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {

                                record = connection.Execute(insert_sql, toImportItem, transaction);

                                transaction.Commit();
                                await UpdateLastChangeDate(DateTime.Now);
                            }
                            catch (Exception )
                            {
                                //Debug.WriteLine(ex.ToString());
                                transaction.Rollback();

                            }
                            finally
                            {
                                
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Debug.WriteLine(ex.ToString());
            }
            finally
            {

            }
            return record;
        }
        #region update lastchange
        public SettingUI GetServerLastChangeDate()
        {
            //Group=[group]='MasterData'
            //Key=LastChangedDate
            string tableName = "G_Setting";

            string sql = "Select * from " + tableName;
            sql += " WHERE [Group]='MasterData' AND [KEY]='LastChangedDate'";


            IDbConnection db = new SqlConnection(ConnectionString);

            SettingUI data = db.QueryFirstOrDefault<SettingUI>(sql);
            return data;
        }
        /// <summary>
        /// update LastChange Date khi update master data thanh cong
        /// </summary>
        /// <param name="ChangeDate"></param>
        public async Task<bool> UpdateLastChangeDate(DateTime ChangeDate)
        {
            bool record = false;


            SettingUI data = GetServerLastChangeDate();
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (data != null)
                {
                    data.Key = ChangeDate.ToString();
                    record = await db.UpdateAsync<SettingUI>(data);
                }
                else
                {
                    // sql += " WHERE [Group]='MasterData' AND [KEY]='LastChangedDate'";
                    data = new SettingUI()
                    {
                        CreatedBy = "System",
                        CreatedOn = DateTime.Now,
                        Description = "Last changed date",
                        Group = "MasterData",
                        Key = "LastChangedDate",
                        ModifiedBy = "System",
                        ModifiedOn = DateTime.Now,
                        Value = ChangeDate.ToString()
                    };
                    record = await db.InsertAsync<SettingUI>(data)>0;
                }
            }
            return record;
        }
        #endregion
    }
}
