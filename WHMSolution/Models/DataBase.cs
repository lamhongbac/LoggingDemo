using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSolution.Models
{
    public class SQLDataBase
    {
        IMapper _mapper;
        IDbConnection connection;
        string _connectionString;
        public SQLDataBase(DBConfig Dbcongig, IMapper mapper)
        {
            _mapper = mapper;
               _connectionString = Dbcongig.GetConnectionString();
            // connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// b1. loai bo cac item dang co trong CSDL ra khoi danh sach
        /// b2. thuc hien trong 1 transaction
        /// b3. tra ve so dong da import, hoac kg import dc gi ca (-1)
        /// </summary>
        /// <param name="inmport_data"></param>
        /// <returns></returns>
        public async Task<int> ImportData(List<MobMasterStockModel> inmport_data)
        {
            int record = -1;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string findExistStockSql = @"SELECT * FROM G_StockMasterBarCode where BarCode In @BarCodeList";
                    List<string> barcodes = inmport_data.Select(x => x.BarCode).ToList();
                    var parameters = new DynamicParameters();
                    parameters.Add("@BarCodeList", barcodes);

                    var existitems = await connection.QueryAsync<MobMasterStockModel>(findExistStockSql, parameters);

                    List<MobMasterStockModel> existStockitems = existitems.ToList();
                    List<MobMasterStockModel> toImportItem = new List<MobMasterStockModel>();

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
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.ToString());
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
               

            }
            finally
            {
                
            }
            return record;
        }
    }
}