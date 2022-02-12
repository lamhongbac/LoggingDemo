using System;
using System.Collections.Generic;

namespace DAL
{
    public class MyDAL<T> : IMyDAL<T>
    {
        public string connectionString;
        public MyDAL(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public void CreateData(T data)
        {

        }
        /// <summary>
        /// doc data
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public T[] ReadData(string whereCondition, Dictionary<string, object> paras)
        {
            return new T[] { };
        }
        public int UpdateData(T data)
        {
            return 1;
        }
        //so dong delete
        public int DeleteData(T data)
        {
            return 1;
        }
    }
}
