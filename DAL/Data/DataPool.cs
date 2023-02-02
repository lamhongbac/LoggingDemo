using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataPool
    {
        public static DataSync SyncManagement { get; }
        public static List<StoreData> StoreDatas { get; }
        public static List<BrandData> BrandDatas { get; }
        public DataPool() { }

        public static bool IsInit(EReload item)
        {
          return  SyncManagement.IsInit(item);
        }

        public void AddStoreData(List<StoreData> newData)
        {
            
                DataPool.StoreDatas.AddRange(newData);
            
        }
        public void UpdateStoreData(List<StoreData> existData)
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
    }
    
}
