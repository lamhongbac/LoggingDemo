using MSA.DAPPER.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class StoreDataService
    {
        private string conectionString = "Data Source=171.244.201.102,8089;Initial Catalog=CLMHQ;User ID=dev;Password=1q2w3e4r;TrustServerCertificate=true";
        public List<StoreData> GetLastData(DateTime lastUpdate)
        {
            string strSQL = "Select * from G_Outlet Where ModifiedOn>@ModifiedOn";
            object para = new { ModifiedOn = lastUpdate };
            GenericDataPortal<StoreData > storeDal = new GenericDataPortal<StoreData>(conectionString,"G_Outlet");
            return storeDal.ReadList(strSQL, para, "ID").Result;
        }
        /// <summary>
        /// lay danh sach tin tuc chu y: kg can lay G_ClientReload , vi no = Max modifiedOn
        /// </summary>
        /// <returns></returns>
        public List<StoreData> InitData()
        {
            string strSQL = "Select * from G_Outlet ";
            //return new List<StoreData>();
            GenericDataPortal<StoreData> storeDal = new GenericDataPortal<StoreData>(conectionString, "G_Outlet");
            return storeDal.ReadList(strSQL, null, "ID").Result;
        }
    }
}
