using MSAMobApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.DataBase
{
   public class DataBaseInit
    {
        public DataBaseInit()
        {
            IsDeletedRequest = false;
        }
        public bool IsDeletedRequest { get; set; }

        public bool DeleteNow()
        {
            bool result = false;
            if (IsDeletedRequest)
            {
                result= MSADataBase.DropAllTables();
            
            }
            return result;
        }
        public bool CreateTables()
        {
            bool result = false;
            return result;
        }
    }
}
