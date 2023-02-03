using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataPool
    {
        public static DataSync SyncManagement { get; private set; }
        public static List<StoreData> StoreDatas { get; private set; }
        public static List<BrandData> BrandDatas { get; set; }
        public DataPool() 
        { 
           
        }

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
