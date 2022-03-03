using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class MyDAL<T> : IMyDAL<T>
    {
        private readonly ILogger<T> logger;
       private readonly IConnnectionStringManager connnectionStringManager;
        public MyDAL(IConnnectionStringManager _connnectionStringManager, ILogger<T> _logger)
        {
            connnectionStringManager = _connnectionStringManager;
            logger = _logger;
        }
        public void CreateData(T data)
        {
            //connnectionStringManager.ConnectionString;
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
            if (connnectionStringManager != null && connnectionStringManager.ConnectionString == "abcconde")
            {
                logger.LogInformation("Update success", data);
                return 1;
            }
            else
            {
                logger.LogError("DB connection failed", connnectionStringManager);
                return -1;
            }
        }
        //so dong delete
        public int DeleteData(T data)
        {
            return 1;
        }
        /// <summary>
        /// object (prop value as para)
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public IEnumerable<T> ReadData(object para)
        {
            Dictionary<string, object> paraDic = convertToDictionary(para);
            return new List<T>();
        }
        public Dictionary<string, object> convertToDictionary(object dtype)
        {

            var props = dtype.GetType().GetProperties();
            var pairDictionary = props.ToDictionary(x => x.Name, x => x.GetValue(props, null));
            return pairDictionary;
        }
    }
}
