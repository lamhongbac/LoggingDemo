using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    /// <summary>
    /// Data pool: la 1 pool chua data
    /// vai tro cua class chi goi gon trong vai tro la 1 noi chua data
    /// 
    /// </summary>
    public class DataPool
    {
        public static DataSync SyncManagement { get; private set; }
        public static List<StoreData> StoreDatas { get; private set; }
        public static List<BrandData> BrandDatas { get; set; }
        public DataPool() 
        { 
           
        }
        /// <summary>
        /// ham init dung de goi boi client khi can 
        /// Data pool chi duoc init 1 lan
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsInit(EReload item)
        {
          return  SyncManagement.IsInit(item);
        }

        public static void AddStoreData(List<StoreData> newData)
        {
            
                DataPool.StoreDatas.AddRange(newData);
            
        }
        public static void UpdateStoreData(List<StoreData> existData)
        {
            foreach (var updateItem in existData)
            {
                var existItem = DataPool.StoreDatas.Where(x => x.Id == updateItem.Id).FirstOrDefault();
                if (existItem != null)
                {
                    var index = DataPool.StoreDatas.IndexOf(existItem);
                    if (index != -1)
                        DataPool.StoreDatas[index] = updateItem;
                }
            }

           

        }

        internal static void CreateTracking()
        {
            SyncManagement = new DataSync();
            StoreDatas = new List<StoreData>();
            BrandDatas = new List<BrandData>();
        }
    }
    
}
