using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GenericDataPortal<T>
    {
        IDbConnection connection;
        string tableName = string.Empty;

        private string _connectionString;
        public GenericDataPortal(string connectionString, string _tableName)

        {
            tableName = _tableName;
            _connectionString = connectionString;

        }


        /// <summary>
        /// Tra ve 1 doi tuong su dung dapper
        /// </summary>
        /// <param name="whereString"></param>
        /// <param name="parametter"></param>
        /// <returns></returns>
        public async Task<T> Read(string whereString, object parametter)
        {
            connection = new SqlConnection(_connectionString);

            //1. Build SQL
            string Sql = "SELECT * FROM " + tableName + " WHERE " + whereString;
            //2. Ket qua tra ve
            T data = await connection.QueryFirstOrDefaultAsync<T>(Sql, parametter);

            return data;
        }

        public async Task<List<T>> ReadList(string whereString, object parametters = null, string orderCommand = "")
        {
            connection = new SqlConnection(_connectionString);
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
                    var data = await connection.QueryAsync<T>(Sql, parametters);
                    return data.ToList();
                }
                else
                {
                    var data = await connection.QueryAsync<T>(Sql);
                    return data.ToList();
                }
            }
            else
            {
                var data = await connection.QueryAsync<T>(Sql);
                return data.ToList();
            }
        }


    }
}
