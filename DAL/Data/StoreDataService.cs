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
            GenericDataPortal<StoreData> storeDal = new GenericDataPortal<StoreData>(conectionString, "G_Outlet");
            List<StoreData> storeDatas= storeDal.ReadList(strSQL, para, "ID").Result;
            List<StoreData> exsit_storeDatas = new List<StoreData>();
            List<StoreData> new_storeDatas = new List<StoreData>();
            foreach (StoreData storeData in storeDatas) 
            {
                if (DataPool.StoreDatas.Where(x=>x.Id==storeData.Id).FirstOrDefault()!=null)
                {
                    exsit_storeDatas.Add(storeData);
                }
                else
                {
                    new_storeDatas.Add(storeData);
                }
            }
            return DataPool.StoreDatas;
        }
        /// <summary>
        /// lay danh sach tin tuc chu y: kg can lay G_ClientReload , vi no = Max modifiedOn
        /// </summary>
        /// <returns></returns>
        public bool InitData()
        {
            string strSQL = "Select * from G_Outlet ";
            //string strSQL1 = "Select * from ClientReload where TableName=@TableName";

            //return new List<StoreData>();
            GenericDataPortal<StoreData> storeDal = new GenericDataPortal<StoreData>(conectionString, "G_Outlet");
            List<StoreData>  storeDatas=storeDal.ReadList(strSQL, null, "ID").Result;
            if (storeDatas != null && storeDatas.Count > 0)
            {
                DateTime lastUpdate = storeDatas.Max(x => x.ModifiedOn).Value;
                DataPool.StoreDatas.AddRange(storeDatas);
                DataPool.SyncManagement.UpdateInitDate(EReload.Outlet, lastUpdate);
                return true;
            }
            return false;
        }
        
    }


    
    
}
