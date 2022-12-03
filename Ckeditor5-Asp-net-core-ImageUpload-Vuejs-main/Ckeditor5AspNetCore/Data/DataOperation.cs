using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NewsCMS.Data
{
    public class DataOperation
    {
        string tableName = "tblNews";
        string _connectionString;
        public DataOperation(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<NewsData>> GetAppList()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM G_APPLIST";
                var result = await connection.QueryAsync<NewsData>(sql);
                List<NewsData> list = result.ToList();
                return list;
            }
        }
        public async Task<List<NewsData>> ReadList(string whereString, object parametters = null, string orderCommand = "")
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string Sql = "SELECT * FROM " + tableName;
                if (!String.IsNullOrEmpty(whereString))
                {
                    string orderByString;
                    if (string.IsNullOrEmpty(orderCommand))
                    {
                        orderByString = "";
                    }
                    else
                    {
                        orderByString = " ORDER BY " + orderCommand;
                    }
                    Sql += " WHERE " + whereString + orderByString;

                    if (parametters != null)
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        var data = await connection.QueryAsync<NewsData>(Sql, parametters);
                        return data.ToList();
                    }
                    else
                    {
                        var data = await connection.QueryAsync<NewsData>(Sql);
                        return data.ToList();
                    }
                }
                else
                {
                    var data = await connection.QueryAsync<NewsData>(Sql);
                    return data.ToList();
                }
            }
        }
        #region CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandUI"></param>
        /// <returns></returns>
        public async Task<bool> UpdateData(NewsData brandUI)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.UpdateAsync<NewsData>(brandUI);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandUI"></param>
        /// <returns></returns>
        public async Task<long> InsertData(NewsData brandUI)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.InsertAsync<NewsData>(brandUI);
                    return result;
                }
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                throw;

            }
        }
        #endregion
    }
}
